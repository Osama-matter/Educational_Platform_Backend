import React, { useState } from 'react';
import { AccountService } from '../services/AccountService';

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await AccountService.login({ email, password });
      console.log('Login successful:', response);
      // Handle successful login (e.g., store token, redirect)
    } catch (error) {
      console.error('Login failed:', error);
    }
  };

  return (
    <div className="hero">
      <div>
        <div className="badge">Member Access</div>
        <h1 className="section-title">Welcome back to Arcadia.</h1>
        <p className="subtle">
          Continue your journey with curated courses, focused mentorship, and progress
          built for real momentum.
        </p>
      </div>
      <div className="hero-card">
        <h2 className="section-title">Sign in</h2>
        <p className="subtle">Use your account to access your learning hub.</p>
        <form className="form" onSubmit={handleLogin}>
          <div>
            <label>Email</label>
            <input
              className="input"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          <div>
            <label>Password</label>
            <input
              className="input"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <button className="btn" type="submit">Login</button>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
