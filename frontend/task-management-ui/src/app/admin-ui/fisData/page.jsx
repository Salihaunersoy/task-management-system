"use client";

import { useState, useEffect } from "react";
import "./fisData.css";

export default function ExcelRead() {

  const [selectedFile, setSelectedFile] = useState(null);
  const [uploading   , setUploading   ] = useState(false);
  const [uploadStatus, setUploadStatus] = useState({ type: "", message: "" });

  const [fisList      , setFisList      ] = useState([]);
  const [pagination   , setPagination   ] = useState({ page: 1, totalPages: 1 });
  const [isListLoading, setIsListLoading] = useState(false);

  const uploadExcelFile     = async  ()        => {

    if (!selectedFile) {
      alert("Lütfen önce bir dosya seçin.");
      return;
    }

    setUploading(true);
    setUploadStatus({ type: "info", message: "Dosya işleniyor, lütfen bekleyin..." });

    const formData = new FormData();
    formData.append("file", selectedFile);

    try {
      const token = localStorage.getItem("token");
      const res   = await fetch("http://localhost:5271/api/Report/upload", {
        method: "POST",
        body: formData,
        headers: {
          "Authorization": `Bearer ${token}`
        },
    });

    if (res.ok) 
    {
      const result = await res.json();
      alert(`Success! ${result.count} rows have been saved to the database.`);
      setSelectedFile(null);
    } else 
    {
      const errorText = await res.text();
      alert("Upload Failed: " + errorText);
    }
    
  } catch (err) {
    console.error("Upload error:", err);
    alert("System Error: Could not reach the server.");
  } finally {
    setUploading(false);
  }

  };
  
  const listFisData         = async (page = 1) => {
    setIsListLoading(true);
    try {
      const token = localStorage.getItem("token");
      const res = await fetch(`http://localhost:5271/api/Report/listFisData?page=${page}&pageSize=100`, {
        headers: {
          "Authorization": `Bearer ${token}`
        }
      });

      if (res.ok) {
        const result = await res.json();
        setFisList(result.data.items);
        setPagination({
          page      : result.data.page,
          totalPages: result.data.totalPages
        });
      } else {
        const errorText = await res.text();
        console.log("API Error:", res.status, errorText);
        alert(`Veriler yüklenemedi. Status: ${res.status}`);
      }
    } catch (err) {
      console.error("Fetch error:", err);
      alert("Sunucuya bağlanılamadı.");
    } finally {
      setIsListLoading(false);
    }
  };

  const exportFisData       = async ()         => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        alert("Session not found. Please log in.");
        return;
      }

      const res = await fetch("http://localhost:5271/api/Report/exportFisData", {
        headers: {
          "Authorization": `Bearer ${token}`
        }
      });

      if (res.ok) 
      {
        const blob = await res.blob();
        const fileName = res.headers.get("Content-Disposition")?.split("filename=")[1]?.replace(/"/g, "") || "FisData.xlsx";

        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = fileName;
        document.body.appendChild(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url);
      } 
      else
      {
        const error = await res.json();
        alert(error.message || "Failed to download Excel file.");
      }
    } catch (err) {
      console.error("Export error:", err);
      alert("Could not connect to the server.");
    }
  };

  const exportFisDataAsWord = async () => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        alert("Session not found. Please log in.");
        return;
      }

      const res = await fetch("http://localhost:5271/api/Report/exportFisDataAsWord", {
        headers: {
          "Authorization": `Bearer ${token}`
        }
      });

      if (res.ok) {
        const blob = await res.blob();
        const fileName = res.headers.get("Content-Disposition")?.split("filename=")[1]?.replace(/"/g, "") || "FisData.docx";

        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = fileName;
        document.body.appendChild(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url);
      } else {
        const errorText = await res.text();
        console.error("Word Export Error:", errorText);
        alert("Failed to download Word file: " + errorText);
      }
    } catch (err) {
      console.error("Export error:", err);
      alert("Could not connect to the server.");
    }
  };

  return (
    <div>
      <div className="reports-row">
         <div className="report-card">
              <div className="report-card-title">
              <h3>Add Excel Fis Data Table</h3>             
              <input
              type="file"
              accept=".xlsx,.xls"
              onChange={(e) => setSelectedFile(e.target.files[0])}
              />
              <button
              type="button"
              className="btn-export blue"
              onClick={uploadExcelFile}
              >
              Upload</button>
            </div>
        </div>
         <div className="report-card">
              <div className="report-card-title">
              <h3>List Excel Fis Data Table</h3>    
              <button
              type="button"
              className="btn-export blue"
              onClick={() => listFisData(1)}
              >List
              </button>         
            </div>
        </div>
         <div className="report-card">
              <div className="report-card-title">
              <h3>Export Fis Data</h3>    
              <button
              type="button"
              className="btn-export blue"
              onClick={exportFisData}
              >Export Excel
              </button>         
            </div>
        </div>
        <div className="report-card">
              <div className="report-card-title">
              <h3>Export Fis Data As Word</h3>    
              <button
              type="button"
              className="btn-export blue"
              onClick={exportFisDataAsWord}
              >Export Word
              </button>         
            </div>
        </div>
      </div>

      <div className="fis-table-wrapper">

       <table className="fis-table">
        <thead>
            <tr>
                <th>Fis No</th>
                <th>Fis Tarihi</th>
                <th>Hesap No</th>
                <th>Hesap Adı</th>
                <th>Aciklama</th>
                <th>Fatura No</th>
                <th>Borc</th>
                <th>Alacak</th>
            </tr>
        </thead>
        <tbody>
        {fisList.map((item) => (
              <tr key={item.fisId}>
                <td>{item.fisNo}</td>
                <td>{item.fisTarih}</td>
                <td>{item.hesapKodu}</td>
                <td>{item.hesapAdi}</td>
                <td>{item.aciklama}</td>
                <td>{item.faturaNo}</td>
                <td>{item.borc}</td>
                <td>{item.alacak}</td>
              </tr>
            ))}
        </tbody>
       </table>

       <div className="pagination">
            <button onClick={() => listFisData(pagination.page - 1)} disabled={pagination.page === 1 || isListLoading}>Geri</button>
            <span>Sayfa {pagination.page} / {pagination.totalPages}</span>
            <button onClick={() => listFisData(pagination.page + 1)} disabled={pagination.page === pagination.totalPages || isListLoading}>İleri</button>
       </div>

      </div>

    </div>
  );
}
