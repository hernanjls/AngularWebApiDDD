export interface Task {
    taskId: number
    name: string
    description: string
    auditCreateDate: Date
    state: number
    stateTask: string
}

export interface TaskApi {
    data: any
    totalRecords: number
}