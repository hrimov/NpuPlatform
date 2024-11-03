export interface NPUCreation {
  id: string;
  title: string;
  description: string;
  imageUrl: string;
  elementIds: string[];
  createdAt: string;
  userId: string;
  totalScore: number;
}

export interface Vote {
  id: string;
  score: number;
  userId: string;
  createdAt: string;
}

export interface ApiResponse<T> {
  data: T;
  message?: string;
  status: number;
}
