"use client";

import { useState, useEffect } from "react";
import PriorityDoughnutChart   from "../../../components/charts/PriorityDoughnutChart";
import StatusDoughnutChart     from "../../../components/charts/TasksPieChart";
import TaskByUserChart         from "../../../components/charts/TaskByUserChart";
import "./admin.css";

export default function AdminHome() {

  const [tasks, setTasks] = useState([]);
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const headers = {
      "Content-Type": "application/json",
      Authorization : `Bearer ${token}`,
    };

    Promise.all([
      fetch("http://localhost:5271/api/Task", { headers }).then((r) => r.json()),
      fetch("http://localhost:5271/api/User", { headers }).then((r) => r.json()),
    ]).then(([taskRes, userRes]) => {
      if (taskRes.success) setTasks(taskRes.data);
      if (userRes.success) setUsers(userRes.data);
    });
  }, []);

  const getCount = (field, value) =>
    tasks.filter((t) => (t[field] || t[field.charAt(0).toUpperCase() + field.slice(1)]) === value).length;

  const kpis = [
    { label: "Total Tasks", value: tasks.length                    , icon: "fa-solid fa-list-check"    , color: "blue"   },
    { label: "To Do"      , value: getCount("status", "ToDo"      ), icon: "fa-solid fa-clipboard-list", color: "red"    },
    { label: "In Progress", value: getCount("status", "InProgress"), icon: "fa-solid fa-spinner"       , color: "yellow" },
    { label: "Testing"    , value: getCount("status", "Testing"   ), icon: "fa-solid fa-flask"         , color: "sky"    },
    { label: "On Hold"    , value: getCount("status", "OnHold"    ), icon: "fa-solid fa-pause"         , color: "orange" },
    { label: "Done"       , value: getCount("status", "Done"      ), icon: "fa-solid fa-circle-check"  , color: "green"  },
  ];

  return (
    <div>
      <div className="kpi-grid">
        {kpis.map((kpi) => (
          <div key={kpi.label} className={`kpi-card ${kpi.color}`}>
            <div className={`kpi-icon ${kpi.color}`}>
              <i className={kpi.icon}></i>
            </div>
            <div>
              <p className="kpi-label">{kpi.label}</p>
              <p className="kpi-value">{kpi.value}</p>
            </div>
          </div>
        ))}
      </div>

      <div className="charts-row-doughnuts">
        <div className="chart-container chart-doughnut">
          <h2 className="chart-title">Priority Distribution</h2>
          <div className="chart-wrapper-doughnut">
            <PriorityDoughnutChart tasks={tasks} />
          </div>
        </div>

        <div className="chart-container chart-doughnut">
          <h2 className="chart-title">Status Distribution</h2>
          <div className="chart-wrapper-doughnut">
            <StatusDoughnutChart tasks={tasks} />
          </div>
        </div>
      </div>

      <div className="chart-container chart-byUser">
        <h2 className="chart-title">Tasks by User</h2>
        <TaskByUserChart tasks={tasks} users={users} />
      </div>
    </div>
  );
}
