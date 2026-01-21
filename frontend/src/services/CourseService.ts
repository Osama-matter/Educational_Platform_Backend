import api from './api';
import { CourseDto, CreateCourseDto, UpdateCourseDto } from '../types/course';

export const CourseService = {
  createCourse: async (data: CreateCourseDto): Promise<CourseDto> => {
    const response = await api.post<CourseDto>('/courses', data);
    return response.data;
  },

  getAllCourses: async (): Promise<CourseDto[]> => {
    const response = await api.get<CourseDto[]>('/courses');
    return response.data;
  },

  getCourseById: async (courseId: string): Promise<CourseDto> => {
    const response = await api.get<CourseDto>(`/courses/${courseId}`);
    return response.data;
  },

  updateCourse: async (courseId: string, data: UpdateCourseDto): Promise<void> => {
    await api.put(`/courses/${courseId}`, data);
  },

  deleteCourse: async (courseId: string): Promise<void> => {
    await api.delete(`/courses/${courseId}`);
  },
};
