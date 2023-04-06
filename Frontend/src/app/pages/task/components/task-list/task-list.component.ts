import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DatesFilter } from '@shared/functions/actions';
import { FiltersBox, SearchOptions } from '@shared/models/search-options.interface';
import { CustomTitleService } from '@shared/services/custom-title.service';
import { fadeInRight400ms } from 'src/@vex/animations/fade-in-right.animation';
import { scaleIn400ms } from 'src/@vex/animations/scale-in.animation';
import { stagger40ms } from 'src/@vex/animations/stagger.animation';
import { TaskApi } from 'src/app/pages/task/models/task-response.interface';
import { TaskService } from 'src/app/pages/task/services/task.service';
import Swal from 'sweetalert2';
import { TaskManageComponent } from '../task-manage/task-manage.component';
import { componentSettings } from './task-list-config';

@Component({
  selector: 'vex-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
  animations: [
    stagger40ms,
    scaleIn400ms,
    fadeInRight400ms
  ]
})
export class TaskListComponent implements OnInit {

  component

  constructor(
    customTitle: CustomTitleService,
    public _taskService: TaskService,
    public _dialog: MatDialog
  ) {
    customTitle.set('Taks')
  }

  ngOnInit(): void {
    this.component = componentSettings
  }

  setData(value: number) {
    this.component.filters.stateFilter = value
    this.formatGetInputs()
  }

  search(data: FiltersBox) {
    this.component.filters.numFilter = data.searchValue
    this.component.filters.textFilter = data.searchData
    this.formatGetInputs()
  }

  datesFilterOpen() {
    DatesFilter(this)
  }

  formatGetInputs() {
    let inputs = {
      numFilter: 0,
      textFilter: "",
      stateFilter: null,
      startDate: null,
      endDate: null
    }

    if (this.component.filters.numFilter != "") {
      inputs.numFilter = this.component.filters.numFilter
      inputs.textFilter = this.component.filters.textFilter
    }

    if (this.component.filters.stateFilter != null) {
      inputs.stateFilter = this.component.filters.stateFilter
    }

    if (this.component.filters.startDate != "" && this.component.filters.endDate != "") {
      inputs.startDate = this.component.filters.startDate
      inputs.endDate = this.component.filters.endDate
    }

    this.component.getInputs = inputs

  }

  openDialogRegister() {
    this._dialog.open(TaskManageComponent, {
      disableClose: true,
      width: '400px'
    }).afterClosed().subscribe(
      (res) => {
        if (res) {
          this.formatGetInputs()
        }
      }
    )
  }

  rowClick(e: any) {
    let action = e.action
    let task = e.row

    switch (action) {
      case "edit":
        this.taskEdit(task)
        break
      case "remove":
        this.taskRemove(task)
        break
    }
    return false
  }

  taskEdit(row: TaskApi) {
    const dialogConfig = new MatDialogConfig()
    dialogConfig.data = row

    let dialogRef = this._dialog.open(TaskManageComponent, {
      data: dialogConfig,
      disableClose: true,
      width: '400px'
    })
    dialogRef
      .afterClosed().subscribe(
        (res) => {
          if (res) {
            this.formatGetInputs()
          }
        }
      )
  }

  taskRemove(task: any) {
    Swal.fire({
      title: `¿You really want to delete the task ${task.name}?`,
      text: "It will be permanently deleted!",
      icon: "warning",
      showCancelButton: true,
      focusCancel: true,
      confirmButtonColor: 'rgb(210, 155, 253)',
      cancelButtonColor: 'rgb(79, 109, 253)',
      confirmButtonText: 'Yes, Delete',
      cancelButtonText: 'Cancel',
      width: 430
    }).then((result) => {
      if (result.isConfirmed) {
        this._taskService.TaskRemove(task.taskId).subscribe(() => this.formatGetInputs())
      }
    })
  }

}