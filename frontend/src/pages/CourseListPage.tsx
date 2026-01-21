import React, { useEffect, useState } from 'react';
import { CourseService } from '../services/CourseService';
import { CourseDto } from '../types/course';

const CourseListPage: React.FC = () => {
  const [courses, setCourses] = useState<CourseDto[]>([]);

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const response = await CourseService.getAllCourses();
        setCourses(response);
      } catch (error) {
        console.error('Failed to fetch courses:', error);
      }
    };

    fetchCourses();
  }, []);

  return (
    <div>
      <section className="hero">
        <div>
          <div className="badge">Curated Library</div>
          <h1 className="section-title">Explore learning journeys</h1>
          <p className="subtle">
            Choose from instructor-led courses designed to build momentum in a focused
            workspace.
          </p>
        </div>
        <div className="hero-card">
          <h2 className="section-title">Your next milestone</h2>
          <p className="subtle">Enroll in a course and track your progress in one place.</p>
          <button className="btn">Start learning</button>
        </div>
      </section>

      <section>
        <h2 className="section-title">Courses</h2>
        {courses.length === 0 ? (
          <div className="empty">No courses available yet.</div>
        ) : (
          <div className="grid">
            {courses.map((course) => (
              <a className="card" key={course.id} href={`/courses/${course.id}`}>
                <h3>{course.title}</h3>
                <p>{course.description || 'A focused learning experience.'}</p>
                <p className="subtle">Duration: {course.estimatedDurationHours} hours</p>
              </a>
            ))}
          </div>
        )}
      </section>
    </div>
  );
};

export default CourseListPage;
