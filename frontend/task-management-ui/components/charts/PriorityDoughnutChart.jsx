"use client";

import { Doughnut } from "react-chartjs-2";
import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend,
} from "chart.js";

ChartJS.register(ArcElement, Tooltip, Legend);

export default function PriorityDoughnutChart({ tasks }) {
  if (!tasks || tasks.length === 0) return null;

  const lowCount      = tasks.filter((t) => (t.priority || t.Priority) === "Low").length;
  const mediumCount   = tasks.filter((t) => (t.priority || t.Priority) === "Medium").length;
  const highCount     = tasks.filter((t) => (t.priority || t.Priority) === "High").length;
  const criticalCount = tasks.filter((t) => (t.priority || t.Priority) === "Critical").length;
  const total         = tasks.length;

  const data = {
    labels  : ["Low", "Medium", "High", "Critical"],
    datasets: [
      {
        data                : [lowCount, mediumCount, highCount, criticalCount],
        backgroundColor     : ["#94a3b8", "#eab308", "#f97316", "#ef4444"],
        spacing             : 3,
        borderRadius        : 0,
      },
    ],
  };

  const options = {
    responsive: true,
    cutout    : "70%",
    plugins   : {
      legend: {
        position: "right",
        labels  : {
          padding      : 14,
          pointStyle   : "circle",
          font         : { size: 12, weight: "500" },
        },
      },
      tooltip: {
        backgroundColor: "#1f2937",
        titleFont      : { size: 13, weight: "600" },
        bodyFont       : { size: 12 },
        padding        : 12,
        callbacks      : 
        {
          label: (ctx) => {
            const pct = ((ctx.raw / total) * 100).toFixed(1);
            return ` ${ctx.label}: ${ctx.raw} (${pct}%)`;
          },
        },
      },
    },
  };

  return <Doughnut data={data} options={options}/>;
}
