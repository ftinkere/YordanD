export interface User {
    id: string,
    username: string,
    displayName: string
}

export interface Task {
    id: string,
    authorId?: string,
    executorId?: string,
    title: string,
    description: string,
    priority?: 'low'|'medium'|'high',
    dueDate?: Date,
    createdAt?: Date,
    updatedAt?: Date,
}

export interface Column {
    id: string,
    name: string,
    tasks: Task[],
}