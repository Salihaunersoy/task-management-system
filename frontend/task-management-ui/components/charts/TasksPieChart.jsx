"use client";

import { Doughnut } from "react-chartjs-2";
import 
{
  Chart as ChartJS,
  ArcElement      ,
  Tooltip         ,
  Legend          ,
} from "chart.js" ;

ChartJS.register(ArcElement, Tooltip, Legend);

export default function StatusDoughnutChart({ tasks }) {
  if (!tasks || tasks.length === 0) return null;

  const todoCount       = tasks.filter((t) => (t.status || t.Status) === "ToDo"      ).length;
  const inProgressCount = tasks.filter((t) => (t.status || t.Status) === "InProgress").length;
  const testingCount    = tasks.filter((t) => (t.status || t.Status) === "Testing"   ).length;
  const onHoldCount     = tasks.filter((t) => (t.status || t.Status) === "OnHold"    ).length;
  const doneCount       = tasks.filter((t) => (t.status || t.Status) === "Done"      ).length;
  const total           = tasks.length;

  const data = {
    labels  : ["To Do", "In Progress", "Testing", "On Hold", "Done"],
    datasets: [
      {
        data                : [todoCount, inProgressCount, testingCount, onHoldCount, doneCount],
        backgroundColor     : ["#ef4444", "#eab308", "#3b82f6", "#f97316", "#22c55e"],
      },
    ],
  };

  const options = {
    cutout : "70%",
    plugins: {
      legend : 
      {
        position: "right",
        labels  :
        {
          padding: 14,
        },
      },
      tooltip: 
      {
        padding  : 12,
        callbacks: {
          label: (ctx) => {
            const pct = ((ctx.raw / total) * 100).toFixed(1);
            return ` ${ctx.label}: ${ctx.raw} (${pct}%)`;
          },
        },
      },
    },
  };

  return <Doughnut data={data} options={options} />;
}
