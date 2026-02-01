"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import "./login.css";

export default function LoginPage() {

  const [email, setEmail]       = useState("");
  const [password, setPassword] = useState("");
  const [error, setError]       = useState("");
  const router                  = useRouter() ;

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    try {

      const res = await fetch("http://localhost:5271/api/Auth/login", {
        method : "POST",
        headers: { "Content-Type": "application/json" },
        body   : JSON.stringify({ email, password }),
      });

      const data = await res.json();
      console.log(data);

      if (!res.ok || !data.success) 
      {
        setError(data.message || "Login failed.");
        return;
      }

      localStorage.setItem("token", data.data.token);
      localStorage.setItem("user" , JSON.stringify(data.data));

      if (data.data.roleId === 0)
      {
        router.push("/admin-ui");
      } 
      else if (data.data.roleId === 1)
      {
        router.push("/users-ui");
      }
      else
      {
        setError("Unknown User Role")
      }

    } 
  catch (err) 
    {
      setError("Could not connect to the server.");
    }
  };

  return (
    <div className="container">

      <div className="login-container">

        <div className="login-header">
          <h1>Task Management System</h1>
          <p> Please sign in to your account</p>
        </div>

        <form onSubmit={handleSubmit} className="login-form">

          <div className="login-email">

            <label
              htmlFor   ="email"
            >E-mail
            </label>

            <input
              type        ="email"
              id          ="email"
              placeholder ="example@gmail.com"
              value       ={email}
              onChange    ={(e) => setEmail(e.target.value)}
              required
            />

          </div>

          <div className="login-password">
            <label
              htmlFor  ="password"
            >Password
            </label>

            <input
              type        ="password"
              id          ="password"
              placeholder ="********"
              value       ={password}
              onChange    ={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          <button
            type     ="submit"
            className="submit-btn"
          >
            Sign In
          </button>

        </form>

        {error && (
          <div className="errorMsg">
            {error}
          </div>
        )}

        <div className="login-footer">
          © Task Management System
        </div>

      </div>

    </div>
  );
}
