"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";
import "./layout.css";

export default function AdminLayout({ children }) {

  const [fullName    , setFullName    ] = useState("");
  const [showSettings, setShowSettings] = useState(false);
  const [settingsForm, setSettingsForm] = useState({ name: "", surname: "", email: "", password: "" });
  const [settingsMsg , setSettingsMsg ] = useState({ text: "", type: "" });

  const router = useRouter();

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    if (user.name) setFullName(`${user.name} ${user.surname || ""}`);
  }, []);

  const openSettings = () => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    setSettingsForm({
      name    : user.name    || "",
      surname : user.surname || "",
      email   : user.email   || "",
      password: "",
    });
    setSettingsMsg({ text: "", type: "" });
    setShowSettings(true);
  };

  const handleSettingsSubmit = async (e) => {

    e.preventDefault();
    setSettingsMsg({ text: "", type: "" });

    const user  = JSON.parse(localStorage.getItem("user") || "{}");
    const token = localStorage.getItem("token");

    const body = {
      name    :  settingsForm.name,
      surname :  settingsForm.surname,
      email   :  settingsForm.email,
      password:  settingsForm.password,
      roleId  :  user.roleId,
    };

    const res = await fetch(`http://localhost:5271/api/User/${user.userId}`, {
      method : "PUT",
      headers: 
      {
        "Content-Type": "application/json",
        Authorization : `Bearer ${token}`,
      },
      body: JSON.stringify(body),
    });

    const data = await res.json();

    if (res.ok && data.success) 
    {
      const updatedUser = { ...user, name: settingsForm.name, surname: settingsForm.surname, email: settingsForm.email };
      localStorage.setItem("user", JSON.stringify(updatedUser));
      setFullName(`${settingsForm.name} ${settingsForm.surname}`);
      setSettingsMsg({ text: "Settings updated successfully.", type: "success" });
    } 
    else 
    {
      setSettingsMsg({ text: data.message || "Update failed.", type: "error" });
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    router.push("/login");
  };

  const menuItems = [
    { name: "Dashboard", href: "/admin-ui"          , icon: "fa-solid fa-chart-column" },
    { name: "Tasks"    , href: "/admin-ui/tasks"    , icon: "fa-solid fa-list-check"   },
    { name: "Users"    , href: "/admin-ui/users"    , icon: "fa-solid fa-users"        },
    { name: "Reports"  , href: "/admin-ui/reports"  , icon: "fa-solid fa-file-invoice" },
  ]; 

  return (
    <div className="container">
      <aside className="sidebar">
        
        <div className="profile-card">
          <div className="profile-icon-container">
            <i className="fa-solid fa-user"></i>
          </div>
          <p>{fullName || "Admin"}</p>
        </div>

        <nav className="sidebar-nav">
          {menuItems.map((item) => (
            <Link
              key       ={item.name}
              href      ={item.href}
              className ="nav-link"
            >
            <i className={item.icon}></i>
              {item.name}
            </Link>
          ))}
        </nav>

        <div className="sidebar-bottom">
          <button className="nav-link settings-btn" onClick={openSettings}>
            <i className="fa-solid fa-gear"></i>Settings
          </button>
          <button className="nav-link logout-btn" onClick={handleLogout}>
            <i className="fa-solid fa-right-from-bracket"></i>Logout
          </button>
        </div>

      </aside>

      <div className="content">
        {children}
      </div>

      {showSettings && (
        <div className="settings-modal-overlay" onClick={() => setShowSettings(false)}>
          <div className="settings-modal" onClick={(e) => e.stopPropagation()}>
            <h2 className="settings-modal-title">Settings</h2>

            {settingsMsg.text && (
              <div className={`settings-msg ${settingsMsg.type}`}>{settingsMsg.text}</div>
            )}

            <form onSubmit={handleSettingsSubmit} className="settings-modal-form">
              <div className="settings-form-row">
                <div className="settings-form-group">
                  <label>Name</label>
                  <input
                    type="text" required
                    value={settingsForm.name}
                    onChange={(e) => setSettingsForm({ ...settingsForm, name: e.target.value })}
                  />
                </div>
                <div className="settings-form-group">
                  <label>Surname</label>
                  <input
                    type="text" required
                    value={settingsForm.surname}
                    onChange={(e) => setSettingsForm({ ...settingsForm, surname: e.target.value })}
                  />
                </div>
              </div>
              <div className="settings-form-group">
                <label>Email</label>
                <input
                  type="email" required
                  value={settingsForm.email}
                  onChange={(e) => setSettingsForm({ ...settingsForm, email: e.target.value })}
                />
              </div>
              <div className="settings-form-group">
                <label>New Password (leave empty to keep current)</label>
                <input
                  type="password"
                  value={settingsForm.password}
                  onChange={(e) => setSettingsForm({ ...settingsForm, password: e.target.value })}
                />
              </div>
              <div className="settings-modal-actions">
                <button type="button" className="btn-settings-cancel" onClick={() => setShowSettings(false)}>
                  Cancel
                </button>
                <button type="submit" className="btn-settings-save">Save Changes</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
