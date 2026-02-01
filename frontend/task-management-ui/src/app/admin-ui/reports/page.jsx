"use client";

import { useState, useEffect } from "react";
import "./reports.css";

export default function ReportsPage() {
  const [tasks, setTasks]     = useState([]);
  const [users, setUsers]     = useState([]);
  const [loading, setLoading] = useState({ users: false, tasks: false });

  const [chartOptions, setChartOptions] = useState({
    statusChart     : false,
    priorityChart   : false,
    taskByUserChart : false,
  });

  const getHeaders = () => {
    const token = localStorage.getItem("token");
    return {
      "Content-Type": "application/json",
      Authorization : `Bearer ${token}`,
    };
  };

  useEffect(() => {
    fetch("http://localhost:5271/api/Task", { headers: getHeaders() })
      .then((r) => r.json())
      .then((data) => { if (data.success) setTasks(data.data); })
      .catch(() => {});

    fetch("http://localhost:5271/api/User", { headers: getHeaders() })
      .then((r) => r.json())
      .then((data) => { if (data.success) setUsers(data.data); })
      .catch(() => {});
  }, []);

  const exportUsersExcel = async () => {
    setLoading((p) => ({ ...p, users: true }));
    try {
      const XLSX = (await import("xlsx")).default || (await import("xlsx"));

      const data = users.map((u, i) => ({
        "#"      : i + 1,
        "Name"   : u.name,
        "Surname": u.surname,
        "Email"  : u.email,
        "Role"   : u.roleId === 0 ? "Admin" : "User",
      }));

      const ws = XLSX.utils.json_to_sheet(data);
      ws["!cols"] = [{ wch: 5 }, { wch: 15 }, { wch: 15 }, { wch: 30 }, { wch: 10 }];
      const wb = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, "Users");
      XLSX.writeFile(wb, "users_report.xlsx");
    } catch {
      alert("Excel export failed.");
    }
    setLoading((p) => ({ ...p, users: false }));
  };

  const exportTasksExcel = async () => {
    setLoading((p) => ({ ...p, tasks: true }));
    try {
      const token = localStorage.getItem("token");

      const params = new URLSearchParams();
      if (chartOptions.statusChart)     params.append("statusChart", "true");
      if (chartOptions.priorityChart)   params.append("priorityChart", "true");
      if (chartOptions.taskByUserChart) params.append("taskByUserChart", "true");

      const res = await fetch(
        `http://localhost:5271/api/Report/export?${params.toString()}`,
        { headers: { Authorization: `Bearer ${token}` } }
      );

      if (!res.ok) throw new Error("Export failed");

      const blob = await res.blob();
      const url  = window.URL.createObjectURL(blob);
      const a    = document.createElement("a");
      a.href     = url;
      a.download = `Rapor_${new Date().toLocaleDateString("tr-TR").replace(/\./g, "-")}.xlsx`;
      a.click();
      window.URL.revokeObjectURL(url);
    } catch {
      alert("Excel export failed.");
    }
    setLoading((p) => ({ ...p, tasks: false }));
  };

  const selectedChartCount = Object.values(chartOptions).filter(Boolean).length;

  const todoCount     = tasks.filter((t) => t.status === "ToDo"      ).length;
  const progressCount = tasks.filter((t) => t.status === "InProgress").length;
  const testingCount  = tasks.filter((t) => t.status === "Testing"   ).length;
  const holdCount     = tasks.filter((t) => t.status === "OnHold"    ).length;
  const doneCount     = tasks.filter((t) => t.status === "Done"      ).length;

  return (
    <div>
      <div className="reports-header">
        <h1 className="reports-title">Reports</h1>
        <p className="reports-subtitle">Export your data as Excel files with optional charts</p>
      </div>

      <div className="reports-row">
        <div className="report-card">
          <div className="report-card-top">
            <div className="report-card-icon blue">
              <i className="fa-solid fa-users"></i>
            </div>
            <div className="report-card-title">
              <h3>Users Report</h3>
              <p>All registered users with roles</p>
            </div>
          </div>

          <div className="report-stats">
            <div className="stat-item">
              <span className="stat-value">{users.length}</span>
              <span className="stat-label">Total</span>
            </div>
            <div className="stat-divider"></div>
            <div className="stat-item">
              <span className="stat-value blue">{users.filter((u) => u.roleId === 0).length}</span>
              <span className="stat-label">Admins</span>
            </div>
            <div className="stat-divider"></div>
            <div className="stat-item">
              <span className="stat-value gray">{users.filter((u) => u.roleId !== 0).length}</span>
              <span className="stat-label">Users</span>
            </div>
          </div>

          <div className="card-spacer"></div>

          <button
            type="button"
            className="btn-export blue"
            onClick={exportUsersExcel}
            disabled={loading.users || users.length === 0}
          >
            <i className="fa-solid fa-file-excel"></i>
            {loading.users ? "Exporting..." : "Export Users"}
          </button>
        </div>

        <div className="report-card">
          <div className="report-card-top">
            <div className="report-card-icon green">
              <i className="fa-solid fa-list-check"></i>
            </div>
            <div className="report-card-title">
              <h3>Tasks Report</h3>
              <p>All tasks with details and charts</p>
            </div>
          </div>

          <div className="report-stats">
            <div className="stat-item">
              <span className="stat-value">{tasks.length}</span>
              <span className="stat-label">Total</span>
            </div>
            <div className="stat-divider"></div>
            <div className="stat-item">
              <span className="stat-value red">{todoCount}</span>
              <span className="stat-label">To Do</span>
            </div>
            <div className="stat-divider"></div>
            <div className="stat-item">
              <span className="stat-value yellow">{progressCount}</span>
              <span className="stat-label">Progress</span>
            </div>
            <div className="stat-divider"></div>
            <div className="stat-item">
              <span className="stat-value sky">{testingCount}</span>
              <span className="stat-label">Testing</span>
            </div>
            <div className="stat-divider"></div>
            <div className="stat-item">
              <span className="stat-value orange">{holdCount}</span>
              <span className="stat-label">On Hold</span>
            </div>
            <div className="stat-divider"></div>
            <div className="stat-item">
              <span className="stat-value green">{doneCount}</span>
              <span className="stat-label">Done</span>
            </div>
          </div>

          <div className="chart-options">
            <div className="chart-options-header">
              <span className="chart-options-title">Include chart sheets</span>
              {selectedChartCount > 0 && (
                <span className="chart-count-badge">{selectedChartCount} selected</span>
              )}
            </div>
            <div className="chart-options-list">
              <label className="chart-option">
                <input
                  type="checkbox"
                  checked={chartOptions.statusChart}
                  onChange={(e) => setChartOptions({ ...chartOptions, statusChart: e.target.checked })}
                />
                <div className="chart-option-info">
                  <i className="fa-solid fa-chart-pie option-icon status-icon"></i>
                  <span>Status Distribution</span>
                </div>
              </label>
              <label className="chart-option">
                <input
                  type="checkbox"
                  checked={chartOptions.priorityChart}
                  onChange={(e) => setChartOptions({ ...chartOptions, priorityChart: e.target.checked })}
                />
                <div className="chart-option-info">
                  <i className="fa-solid fa-chart-pie option-icon priority-icon"></i>
                  <span>Priority Distribution</span>
                </div>
              </label>
              <label className="chart-option">
                <input
                  type="checkbox"
                  checked={chartOptions.taskByUserChart}
                  onChange={(e) => setChartOptions({ ...chartOptions, taskByUserChart: e.target.checked })}
                />
                <div className="chart-option-info">
                  <i className="fa-solid fa-chart-bar option-icon user-icon"></i>
                  <span>Tasks by User</span>
                </div>
              </label>
            </div>
          </div>

          <div className="card-spacer"></div>

          <button
            type="button"
            className="btn-export green"
            onClick={exportTasksExcel}
            disabled={loading.tasks || tasks.length === 0}
          >
            <i className="fa-solid fa-file-excel"></i>
            {loading.tasks ? "Exporting..." : "Export Tasks"}
          </button>
        </div>
      </div>
    </div>
  );
}
