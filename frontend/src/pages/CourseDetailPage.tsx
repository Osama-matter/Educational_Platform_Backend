import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { CourseService } from '../services/CourseService';
import { CourseDto } from '../types/course';

const CourseDetailPage: React.FC = () => {
  const { courseId } = useParams<{ courseId: string }>();
  const [course, setCourse] = useState<CourseDto | null>(null);

  useEffect(() => {
    const fetchCourse = async () => {
      if (courseId) {
        try {
          const response = await CourseService.getCourseById(courseId);
          setCourse(response);
        } catch (error) {
          console.error('Failed to fetch course:', error);
        }
      }
    };

    fetchCourse();
  }, [courseId]);

  if (!course) {
    return <div className="empty">Loading course details...</div>;
  }

  return (
    <div className="split">
      <div className="hero-card">
        <div className="badge">Course Overview</div>
        <h2 className="section-title">{course.title}</h2>
        <p className="subtle">{course.description}</p>
        <button className="btn">Enroll now</button>
      </div>
      <div className="card">
        <h3>Details</h3>
        <p className="subtle">Instructor: {course.instructorId}</p>
        <p className="subtle">Duration: {course.estimatedDurationHours} hours</p>
        <p className="subtle">Status: {course.isActive ? 'Active' : 'Paused'}</p>
        <button className="btn btn-outline">Download syllabus</button>
      </div>
    </div>
  );
};

export default CourseDetailPage;
