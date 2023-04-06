import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import icClose from '@iconify/icons-ic/twotone-close';
import { AlertService } from '@shared/services/alert.service';
import { TaskService } from 'src/app/pages/task/services/task.service';
import * as configs from '../../../../../static-data/configs';

@Component({
  selector: 'vex-task-manage',
  templateUrl: './task-manage.component.html',
  styleUrls: ['./task-manage.component.scss']
})
export class TaskManageComponent implements OnInit {

  icClose = icClose
  configs = configs


  form: FormGroup

  initForm(): void {
    this.form = this._fb.group({
      taskId: [0, [Validators.required]],
      name: ['', [Validators.required]],
      description: [''],
      state: ['', [Validators.required]]
    })
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data,
    private _fb: FormBuilder,
    private _alert: AlertService,
    private _taskService: TaskService,
    public _dialogRef: MatDialogRef<TaskManageComponent>
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    if(this.data != null){
      console.log(this.data)
      this.taskById(this.data.data.taskId)
    }
  }

  taskById(taskId: number): void {
    this._taskService.TaskById(taskId).subscribe(
      (resp) => {
        this.form.reset({
          taskId: resp.taskId,
          name: resp.name,
          description: resp.description,
          state: resp.state
        })
      }
    )
  }

  taskSave(): void {
    if (this.form.invalid) {
      return Object.values(this.form.controls).forEach((controls) => {
        controls.markAllAsTouched();
      })
    }

    const taskId = this.form.get('taskId').value

    if (taskId > 0) {
      this.taskEdit(taskId)
    } else {
      this.taskRegister()
    }
  }

  taskRegister(): void {
    this._taskService.TaskRegister(this.form.value).subscribe((resp) => {
      if (resp.isSuccess) {
        this._alert.success('Excellent', resp.message)
        this._dialogRef.close(true)
      } else {
        this._alert.warn('Atención', resp.message);
      }
    })
  }

  taskEdit(taskId: number): void {
    this._taskService.TaskEdit(taskId, this.form.value).subscribe((resp) => {
      if(resp.isSuccess){
        this._alert.success('Excellent', resp.message)
        this._dialogRef.close(true)
      }else{
        this._alert.warn('Atención', resp.message);
      }
    })
  }

}
