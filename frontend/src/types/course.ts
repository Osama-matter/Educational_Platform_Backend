export interface CourseDto {
  id: string;
  title: string;
  description: string;
  instructorId: string;
  estimatedDurationHours: number;
  isActive: boolean;
}

export interface CreateCourseDto {
  title: string;
  description: string;
  instructorId: string;
  estimatedDurationHours: number;
  isActive: boolean;
}

export interface UpdateCourseDto {
  title: string;
  description: string;
  estimatedDurationHours: number;
  isActive: boolean;
}
