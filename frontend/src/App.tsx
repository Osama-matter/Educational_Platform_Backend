import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import CourseListPage from './pages/CourseListPage';
import CourseDetailPage from './pages/CourseDetailPage';

function App() {
  return (
    <Router>
      <div className="app-shell">
        <header className="app-header">
          <div className="brand">Arcadia Learning</div>
          <nav className="nav-links">
            <Link to="/courses">Courses</Link>
            <Link to="/login">Login</Link>
            <Link to="/register">Register</Link>
          </nav>
        </header>

        <main className="app-main">
          <Routes>
            <Route path="/" element={<Navigate to="/courses" replace />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/courses" element={<CourseListPage />} />
            <Route path="/courses/:courseId" element={<CourseDetailPage />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
