import React, { useState } from 'react';
import { AccountService } from '../services/AccountService';

const RegisterPage: React.FC = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await AccountService.register({
        username,
        email,
        password,
        firstName,
        lastName,
      });
      console.log('Registration successful:', response);
      // Handle successful registration (e.g., redirect to login)
    } catch (error) {
      console.error('Registration failed:', error);
    }
  };

  return (
    <div className="hero">
      <div>
        <div className="badge">New Account</div>
        <h1 className="section-title">Start a guided learning path.</h1>
        <p className="subtle">
          Build the skills that matter with curated tracks, instructor feedback, and
          progress insights tailored for you.
        </p>
      </div>
      <div className="hero-card">
        <h2 className="section-title">Create account</h2>
        <p className="subtle">Join the cohort and unlock your learning workspace.</p>
        <form className="form" onSubmit={handleRegister}>
          <div>
            <label>Username</label>
            <input
              className="input"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </div>
          <div>
            <label>First name</label>
            <input
              className="input"
              type="text"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
              required
            />
          </div>
          <div>
            <label>Last name</label>
            <input
              className="input"
              type="text"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
              required
            />
          </div>
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
          <button className="btn" type="submit">Register</button>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;
