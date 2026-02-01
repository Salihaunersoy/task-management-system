"use client";

import { useState, useEffect } from "react";

import "./tasks.css"

export default function UserHome() {
  const [tasks, setTasks] = useState([]);

  const getHeaders = () => {
    const token = localStorage.getItem("token");
    return {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    };
  };

  const fetchMyTasks = () => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    fetch("http://localhost:5271/api/Task", { headers: getHeaders() })
      .then((r) => r.json())
      .then((data) => {
        if (data.success) {
          const myTasks = data.data.filter(
            (t) => t.assignedUserId === user.userId
          );
          setTasks(myTasks);
        }
      });
  };

  useEffect(() => {
    fetchMyTasks();
  }, []);

  const changeStatus = async (taskId, newStatus) => {
    await fetch(`http://localhost:5271/api/Task/${taskId}/status`, {
      method: "PUT",
      headers: getHeaders(),
      body: JSON.stringify({ status: newStatus }),
    });
    fetchMyTasks();
  };

  const formatDate = (dateStr) => {
    if (!dateStr) return "-";
    return new Date(dateStr).toLocaleDateString("tr-TR");
  };

  const statusLabel = (s) => {
    if (s === "ToDo")       return "To Do";
    if (s === "InProgress") return "In Progress";
    if (s === "Testing")    return "Testing";
    if (s === "OnHold")     return "On Hold";
    if (s === "Done")       return "Done";
    return s;
  };

  const statusClass = (s) => {
    if (s === "ToDo")       return "card-todo";
    if (s === "InProgress") return "card-progress";
    if (s === "Testing")    return "card-testing";
    if (s === "OnHold")     return "card-onhold";
    if (s === "Done")       return "card-done";
    return "";
  };

  const priorityClass = (p) => {
    if (p === "Low")      return "priority-low";
    if (p === "Medium")   return "priority-medium";
    if (p === "High")     return "priority-high";
    if (p === "Critical") return "priority-critical";
    return "";
  };

  return (
    <div className="tasks-container">
      <h1 className="tasks-title">My Tasks</h1>

      {tasks.length === 0 && (
        <p className="tasks-empty">No tasks assigned to you.</p>
      )}

      <div className="tasks-cards">
        {tasks.map((t) => (
          <div key={t.taskId} className={`tasks-card ${statusClass(t.status)}`}>
            <div className="tasks-card-header">
              <h3 className="tasks-card-title">{t.title}</h3>
              <span className={`task-badge ${statusClass(t.status)}`}>
                {statusLabel(t.status)}
              </span>
            </div>

            {t.description && (
              <p className="tasks-card-desc">{t.description}</p>
            )}

            <div className="tasks-card-meta">
              <div className="tasks-card-date">
                <i className="fa-regular fa-calendar"></i>
                {formatDate(t.dueDate)}
              </div>
              <span className={`tasks-priority-badge ${priorityClass(t.priority)}`}>
                {t.priority || "Medium"}
              </span>
            </div>

            <div className="tasks-card-actions">
              {t.status !== "ToDo" && (
                <button
                  className="tasks-btn tasks-btn-todo"
                  onClick={() => changeStatus(t.taskId, "ToDo")}
                >
                  To Do
                </button>
              )}
              {t.status !== "InProgress" && (
                <button
                  className="tasks-btn tasks-btn-progress"
                  onClick={() => changeStatus(t.taskId, "InProgress")}
                >
                  In Progress
                </button>
              )}
              {t.status !== "Testing" && (
                <button
                  className="tasks-btn tasks-btn-testing"
                  onClick={() => changeStatus(t.taskId, "Testing")}
                >
                  Testing
                </button>
              )}
              {t.status !== "OnHold" && (
                <button
                  className="tasks-btn tasks-btn-onhold"
                  onClick={() => changeStatus(t.taskId, "OnHold")}
                >
                  On Hold
                </button>
              )}
              {t.status !== "Done" && (
                <button
                  className="tasks-btn tasks-btn-done"
                  onClick={() => changeStatus(t.taskId, "Done")}
                >
                  Done
                </button>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
