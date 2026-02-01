"use client";

import { useState, useEffect } from "react";
import "./users.css";

export default function UsersPage() {
  const [users, setUsers]             = useState([]);
  const [showModal, setShowModal]     = useState(false);
  const [editingUser, setEditingUser] = useState(null);
  const [form, setForm]               = useState({ name: "", surname: "", email: "", password: "", roleId: "1" });

  const getHeaders = () => {
    const token = localStorage.getItem("token");
    return {
      "Content-Type": "application/json",
      Authorization : `Bearer ${token}`,
    };
  };

  const fetchUsers = () => {
    fetch("http://localhost:5271/api/User", { headers: getHeaders() })
      .then((r) => r.json())
      .then((data) => { if (data.success) setUsers(data.data); });
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const openAddModal = () => {
    setEditingUser(null);
    setForm({ name: "", surname: "", email: "", password: "", roleId: "1" });
    setShowModal(true);
  };

  const openEditModal = (user) => {
    setEditingUser(user);
    setForm({
      name    : user.name,
      surname : user.surname,
      email   : user.email,
      password: "",
      roleId  : String(user.roleId),
    });
    setShowModal(true);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const body = {
      name    : form.name,
      surname : form.surname,
      email   : form.email,
      password: form.password,
      roleId  : parseInt(form.roleId),
    };

    if (editingUser) {
      await fetch(`http://localhost:5271/api/User/${editingUser.userId}`, {
        method: "PUT", headers: getHeaders(), body: JSON.stringify(body),
      });
    } else {
      await fetch("http://localhost:5271/api/User", {
        method: "POST", headers: getHeaders(), body: JSON.stringify(body),
      });
    }

    setShowModal(false);
    fetchUsers();
  };

  const handleDelete = async (id) => {
    if (!confirm("Bu kullaniciyi silmek istediginize emin misiniz?")) return;
    await fetch(`http://localhost:5271/api/User/${id}`, { method: "DELETE", headers: getHeaders() });
    fetchUsers();
  };

  return (
    <div>
      <div className="users-header">
        <h1 className="users-title">Users</h1>
        <button className="btn-add" onClick={openAddModal}>
          + New User
        </button>
      </div>

      <div className="table-wrapper">
        <table className="users-table">
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Surname</th>
              <th>Email</th>
              <th>Role</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {users.map((u) => (
              <tr key={u.userId}>
                <td>{u.userId}</td>
                <td>{u.name}</td>
                <td>{u.surname}</td>
                <td>{u.email}</td>
                <td>
                  <span className={`role-badge ${u.roleId === 0 ? "role-admin" : "role-user"}`}>
                    {u.roleId === 0 ? "Admin" : "User"}
                  </span>
                </td>
                <td className="actions-cell">
                  <button className="btn-action" onClick={() => openEditModal(u)}>Edit</button>
                  <button className="btn-action btn-delete" onClick={() => handleDelete(u.userId)}>Delete</button>
                </td>
              </tr>
            ))}
            {users.length === 0 && (
              <tr><td colSpan="6" className="empty-row">No users found.</td></tr>
            )}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={(e) => e.stopPropagation()}>
            <h2 className="modal-title">{editingUser ? "Edit User" : "New User"}</h2>
            <form onSubmit={handleSubmit} className="modal-form">
              <div className="form-row">
                <div className="form-group">
                  <label>Name</label>
                  <input
                    type="text" required
                    value={form.name}
                    onChange={(e) => setForm({ ...form, name: e.target.value })}
                  />
                </div>
                <div className="form-group">
                  <label>Surname</label>
                  <input
                    type="text" required
                    value={form.surname}
                    onChange={(e) => setForm({ ...form, surname: e.target.value })}
                  />
                </div>
              </div>
              <div className="form-group">
                <label>Email</label>
                <input
                  type="email" required
                  value={form.email}
                  onChange={(e) => setForm({ ...form, email: e.target.value })}
                />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>{editingUser ? "New Password (leave empty to keep)" : "Password"}</label>
                  <input
                    type="password"
                    required={!editingUser}
                    value={form.password}
                    onChange={(e) => setForm({ ...form, password: e.target.value })}
                  />
                </div>
                <div className="form-group">
                  <label>Role</label>
                  <select
                    value={form.roleId}
                    onChange={(e) => setForm({ ...form, roleId: e.target.value })}
                  >
                    <option value="1">User</option>
                    <option value="0">Admin</option>
                  </select>
                </div>
              </div>
              <div className="modal-actions">
                <button type="button" className="btn-cancel" onClick={() => setShowModal(false)}>Cancel</button>
                <button type="submit" className="btn-save">{editingUser ? "Update" : "Create"}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
