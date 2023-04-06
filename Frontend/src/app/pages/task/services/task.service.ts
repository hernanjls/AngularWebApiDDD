import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AlertService } from '@shared/services/alert.service';
import { Observable } from 'rxjs';
import { Task, TaskApi } from '../models/task-response.interface';
import { environment as env } from 'src/environments/environment';
import { endpoint } from '@shared/apis/endpoint';
import { map } from 'rxjs/operators';
import { TaskRequest } from '../models/task-request.interface';
import { ApiResponse } from '../../../commons/response.interface';
import { getIcon } from '@shared/functions/helpers';
import { ListTaskRequest } from '../models/list-task-request.interface';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(
    private _http: HttpClient,
    private _alert: AlertService
  ) { }

  GetAll(
    size,
    sort,
    order,
    page,
    getInputs
  ): Observable<TaskApi> {

    const requestUrl = `${env.api}${endpoint.LIST_TASKS}`
    const params: ListTaskRequest = new ListTaskRequest(
      page + 1,
      order,
      sort,
      size,
      getInputs.numFilter,
      getInputs.textFilter,
      getInputs.stateFilter,
      getInputs.startDate,
      getInputs.endDate
    )

    return this._http.post<TaskApi>(requestUrl, params).pipe(
      map((data: TaskApi) => {
        data.data.items.forEach(function (e: any) {
          switch (e.state) {
            case 0:
              e.badgeColor = 'text-gray bg-gray-light'
              break
            case 1:
              e.badgeColor = 'text-green bg-green-light'
              break
            default:
              e.badgeColor = 'text-gray bg-gray-light'
              break
          }
          e.icEdit = getIcon("icEdit", "Edit Task", true, "edit");
          e.icDelete = getIcon("icDelete", "Delete Task", true, "remove");
        })
        return data
      })
    )
  }

  TaskRegister(task: TaskRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpoint.TASK_REGISTER}`
    return this._http.post(requestUrl, task).pipe(
      map((resp: ApiResponse) => {
        return resp
      })
    )
  }

  TaskById(TaskId: number): Observable<Task> {
    const requestUrl = `${env.api}${endpoint.TASK_BY_ID}${TaskId}`
    return this._http.get(requestUrl).pipe(
      map((resp: ApiResponse) => {
        return resp.data
      })
    )
  }

  TaskEdit(TaskId: number, task: TaskRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpoint.TASK_EDIT}${TaskId}`
    return this._http.put(requestUrl, task).pipe(
      map((resp: ApiResponse) => {
        return resp
      })
    )
  }

  TaskRemove(TaskId: number): Observable<void> {
    const requestUrl = `${env.api}${endpoint.TASK_REMOVE}${TaskId}`
    return this._http.put(requestUrl, '').pipe(
      map((resp: ApiResponse) => {
        if (resp.isSuccess) {
          this._alert.success('Excellent', resp.message)
        }
      })
    )
  }
}