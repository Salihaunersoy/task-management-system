"use client";

import { Bar } from "react-chartjs-2";
import 
{
  Chart as ChartJS,
  BarElement      ,
  CategoryScale   ,  
  LinearScale     ,
  Tooltip         ,
  Legend          ,
} from "chart.js" ;

ChartJS.register(BarElement, CategoryScale, LinearScale, Tooltip, Legend);

export default function TaskByUserChart({ tasks, users }) {
  if (!tasks || !users || tasks.length === 0 || users.length === 0) return null;

  const nonAdminUsers = users.filter((u) => u.roleId !== 0);
  const labels        = nonAdminUsers.map((u) => `${u.name} ${u.surname}`);

  const statusConfig = [
    { key: "ToDo",       label: "To Do",       bg: "#ef4444", hover: "#dc2626" },
    { key: "InProgress", label: "In Progress", bg: "#eab308", hover: "#ca8a04" },
    { key: "Testing",    label: "Testing",     bg: "#3b82f6", hover: "#2563eb" },
    { key: "OnHold",     label: "On Hold",     bg: "#f97316", hover: "#ea580c" },
    { key: "Done",       label: "Done",        bg: "#22c55e", hover: "#16a34a" },
  ];

  const datasets = statusConfig.map((s) => ({
    label               : s.label,

    data: nonAdminUsers.map((u) => {
      const userTasks = tasks.filter((t) => (t.assignedUserId || t.AssignedUserId) === u.userId);
      return userTasks.filter((t) => (t.status || t.Status) === s.key).length;
    }),

    backgroundColor     : s.bg,
    hoverBackgroundColor: s.hover,
    borderRadius        : 4,
    borderSkipped       : false,
  }));

  const data = { labels, datasets };

  const options = {
    responsive         : true,
    maintainAspectRatio: false,
    plugins            : 
    {
      legend :{position: "top", },
      tooltip: {
        padding: 12,
        mode: "index",
        intersect: false,
      },
    },
    datasets           : 
    {
      bar: { barThickness: 16, borderRadius: 4 },
    },
    scales             : 
    {
      x: 
      {
        stacked: true,
        title  : 
        {
          display: true,
          text   : "Users",
          font   : { size: 13, weight: "600" },
          color  : "#000000",
          padding: { top: 10 },
        },
        ticks: 
        {
          font : { size: 12, weight: "600" },
          color: "#000000",
        },
      },
      y: 
      {
        stacked    : true,
        beginAtZero: true,
        title      : 
        {
          display: true,
          text   : "Task Count",
          font   : { size: 13, weight: "600" },
          color  : "#000000",
          padding: { bottom: 10 },
        },
        ticks: {
          stepSize: 1,
          font    : { size: 12, weight: "500" },
          color   : "#000000",
        },
      },
    },
  };

  return (
    <div style={{ height: "350px" }}>
      <Bar data={data} options={options} />
    </div>
  );
}
