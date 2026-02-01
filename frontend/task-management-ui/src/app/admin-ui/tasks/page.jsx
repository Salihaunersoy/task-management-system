"use client";

import { useState, useEffect } from "react";
import "./tasks.css";

export default function TasksPage() {
  const [tasks, setTasks]             = useState([]);
  const [users, setUsers]             = useState([]);
  const [showModal, setShowModal]     = useState(false);
  const [editingTask, setEditingTask] = useState(null);
  const [form, setForm]               = useState({
    title: "", description: "", status: "ToDo", priority: "Medium", assignedUserId: "", dueDate: "",
  });

  const getHeaders = () => {
    const token = localStorage.getItem("token");
    return {
      "Content-Type": "application/json",
      Authorization : `Bearer ${token}`,
    };
  };

  const fetchTasks = () => {
    fetch("http://localhost:5271/api/Task", { headers: getHeaders() })
      .then((r) => r.json())
      .then((data) => { if (data.success) setTasks(data.data); });
  };

  const fetchUsers = () => {
    fetch("http://localhost:5271/api/User", { headers: getHeaders() })
      .then((r) => r.json())
      .then((data) => { if (data.success) setUsers(data.data); });
  };

  useEffect(() => {
    fetchTasks();
    fetchUsers();
  }, []);

  const getUserName = (id) => {
    const u = users.find((u) => u.userId === id);
    return u ? `${u.name} ${u.surname}` : "-";
  };

  const formatDate = (dateStr) => {
    if (!dateStr) return "-";
    return new Date(dateStr).toLocaleDateString("tr-TR");
  };

  const statusLabel = (s) => {
    if (s === "ToDo"      )  return "To Do"     ;
    if (s === "InProgress") return "In Progress";
    if (s === "Testing"   ) return "Testing"    ;
    if (s === "OnHold"    ) return "On Hold"    ;
    if (s === "Done"      ) return "Done"       ;
    return s;
  };

  const statusClass = (s) => {
    if (s === "ToDo"      )  return "status-todo"   ;
    if (s === "InProgress") return "status-progress";
    if (s === "Testing"   ) return "status-testing" ;
    if (s === "OnHold"    ) return "status-onhold"  ;
    if (s === "Done"      ) return "status-done"    ;
    return "";
  };

  const priorityClass = (p) => {
    if (p === "Low"     )   return "priority-low"     ;
    if (p === "Medium"  )   return "priority-medium"  ;
    if (p === "High"    )   return "priority-high"    ;
    if (p === "Critical")   return "priority-critical";
    return "";
  };

  const openAddModal = () => {
    setEditingTask(null);
    setForm({ title: "", description: "", status: "ToDo", priority: "Medium", assignedUserId: "", dueDate: "" });
    setShowModal(true);
  };

  const openEditModal = (task) => {
    setEditingTask(task);
    setForm({
      title         : task.title,
      description   : task.description || "",
      status        : task.status,
      priority      : task.priority || "Medium",
      assignedUserId: task.assignedUserId,
      dueDate       : task.dueDate ? task.dueDate.split("T")[0] : "",
    });
    setShowModal(true);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const userData = JSON.parse(localStorage.getItem("user") || "{}");
    const body = {
      title            : form.title,
      description      : form.description,
      status           : form.status,
      priority         : form.priority,
      assignedUserId   : parseInt(form.assignedUserId),
      createdByAdminId : userData.userId,
      dueDate          : form.dueDate || null,
    };

    if (editingTask) {
      await fetch(`http://localhost:5271/api/Task/${editingTask.taskId}`, {
        method: "PUT", headers: getHeaders(), body: JSON.stringify(body),
      });
    } else {
      await fetch("http://localhost:5271/api/Task", {
        method: "POST", headers: getHeaders(), body: JSON.stringify(body),
      });
    }

    setShowModal(false);
    fetchTasks();
  };

  const handleDelete = async (id) => {
    if (!confirm("Bu gorevi silmek istediginize emin misiniz?")) return;
    await fetch(`http://localhost:5271/api/Task/${id}`, { method: "DELETE", headers: getHeaders() });
    fetchTasks();
  };

  return (
    <div>
      <div className="tasks-header">
        <h1 className="tasks-title">Tasks</h1>
        <button className="btn-add" onClick={openAddModal}>
          + New Task
        </button>
      </div>

      <div className="table-wrapper">
        <table className="tasks-table">
          <thead>
            <tr>
              <th>#</th>
              <th>Title</th>
              <th>Description</th>
              <th>Status</th>
              <th>Priority</th>
              <th>Assigned To</th>
              <th>Due Date</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {tasks.map((t) => (
              <tr key={t.taskId}>
                <td>{t.taskId}</td>
                <td>{t.title}</td>
                <td>{t.description || "-"}</td>
                <td>
                  <span className={`status-badge ${statusClass(t.status)}`}>
                    {statusLabel(t.status)}
                  </span>
                </td>
                <td>
                  <span className={`status-badge ${priorityClass(t.priority)}`}>
                    {t.priority || "Medium"}
                  </span>
                </td>
                <td>{getUserName(t.assignedUserId)}</td>
                <td>{formatDate(t.dueDate)}</td>
                <td className="actions-cell">
                  <button className="btn-action" onClick={() => openEditModal(t)}>
                    Edit
                  </button>
                  <button className="btn-action" onClick={() => handleDelete(t.taskId)}>
                    Delete
                  </button>
                </td>
              </tr>
            ))}
            {tasks.length === 0 && (
              <tr><td colSpan="8" className="empty-row">No tasks found.</td></tr>
            )}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={(e) => e.stopPropagation()}>
            <h2 className="modal-title">{editingTask ? "Edit Task" : "New Task"}</h2>
            <form onSubmit={handleSubmit} className="modal-form">
              <div className="form-group">
                <label>Title</label>
                <input
                  type="text" required
                  value={form.title}
                  onChange={(e) => setForm({ ...form, title: e.target.value })}
                />
              </div>
              <div className="form-group">
                <label>Description</label>
                <textarea
                  rows="3"
                  value={form.description}
                  onChange={(e) => setForm({ ...form, description: e.target.value })}
                />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Status</label>
                  <select
                    value={form.status}
                    onChange={(e) => setForm({ ...form, status: e.target.value })}
                  >
                    <option value="ToDo">To Do</option>
                    <option value="InProgress">In Progress</option>
                    <option value="Testing">Testing</option>
                    <option value="OnHold">On Hold</option>
                    <option value="Done">Done</option>
                  </select>
                </div>
                <div className="form-group">
                  <label>Priority</label>
                  <select
                    value={form.priority}
                    onChange={(e) => setForm({ ...form, priority: e.target.value })}
                  >
                    <option value="Low">Low</option>
                    <option value="Medium">Medium</option>
                    <option value="High">High</option>
                    <option value="Critical">Critical</option>
                  </select>
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Assigned To</label>
                  <select
                    required
                    value={form.assignedUserId}
                    onChange={(e) => setForm({ ...form, assignedUserId: e.target.value })}
                  >
                    <option value="">Select user...</option>
                    {users.filter((u) => u.roleId !== 0).map((u) => (
                      <option key={u.userId} value={u.userId}>
                        {u.name} {u.surname}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="form-group">
                  <label>Due Date</label>
                  <input
                    type="date"
                    value={form.dueDate}
                    onChange={(e) => setForm({ ...form, dueDate: e.target.value })}
                  />
                </div>
              </div>
              <div className="modal-actions">
                <button type="button" className="btn-cancel" onClick={() => setShowModal(false)}>
                  Cancel
                </button>
                <button type="submit" className="btn-save">
                  {editingTask ? "Update" : "Create"}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
