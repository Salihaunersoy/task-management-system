"use client";

import { useState, useEffect } from "react";
import "./settings.css";

export default function SettingsPage() {
  const [form, setForm] = useState({ name: "", surname: "", email: "", password: "" });
  const [msg, setMsg]   = useState({ text: "", type: "" });

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    setForm({
      name:     user.name    || "",
      surname:  user.surname || "",
      email:    user.email   || "",
      password: "",
    });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMsg({ text: "", type: "" });

    const user  = JSON.parse(localStorage.getItem("user") || "{}");
    const token = localStorage.getItem("token");

    const body = {
      name:     form.name,
      surname:  form.surname,
      email:    form.email,
      password: form.password,
      roleId:   user.roleId,
    };

    const res = await fetch(`http://localhost:5271/api/User/${user.userId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(body),
    });

    const data = await res.json();

    if (res.ok && data.success) {
      const updatedUser = { ...user, name: form.name, surname: form.surname, email: form.email };
      localStorage.setItem("user", JSON.stringify(updatedUser));
      setMsg({ text: "Settings updated successfully.", type: "success" });
    } else {
      setMsg({ text: data.message || "Update failed.", type: "error" });
    }
  };

  return (
    <div>
      <h1 className="settings-title">Settings</h1>

      <div className="settings-card">
        {msg.text && (
          <div className={`settings-msg ${msg.type}`}>{msg.text}</div>
        )}

        <form onSubmit={handleSubmit} className="settings-form">
          <div className="settings-form-row">
            <div className="settings-form-group">
              <label>Name</label>
              <input
                type="text" required
                value={form.name}
                onChange={(e) => setForm({ ...form, name: e.target.value })}
              />
            </div>
            <div className="settings-form-group">
              <label>Surname</label>
              <input
                type="text" required
                value={form.surname}
                onChange={(e) => setForm({ ...form, surname: e.target.value })}
              />
            </div>
          </div>
          <div className="settings-form-group">
            <label>Email</label>
            <input
              type="email" required
              value={form.email}
              onChange={(e) => setForm({ ...form, email: e.target.value })}
            />
          </div>
          <div className="settings-form-group">
            <label>New Password (leave empty to keep current)</label>
            <input
              type="password"
              value={form.password}
              onChange={(e) => setForm({ ...form, password: e.target.value })}
            />
          </div>
          <div className="settings-actions">
            <button type="submit" className="btn-settings-save">Save Changes</button>
          </div>
        </form>
      </div>
    </div>
  );
}
