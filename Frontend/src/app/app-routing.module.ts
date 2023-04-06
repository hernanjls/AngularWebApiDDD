import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule } from '@angular/router';
import { VexRoutes } from 'src/@vex/interfaces/vex-route.interface';
import { CustomLayoutComponent } from './custom-layout/custom-layout.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';

const childrenRoutes: VexRoutes = [
  {
    path: 'dashboard',
    loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashboardModule),
    data: {
      containerEnabled: true
    }
  },
  {
    path: 'tasks',
    loadChildren: () => import('./pages/task/task.module').then(m => m.TaskModule),
    data: {
      containerEnabled: true
    }
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

const routes: VexRoutes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: '',
    component: CustomLayoutComponent,
    children: childrenRoutes,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules,
    scrollPositionRestoration: 'enabled',
    relativeLinkResolution: 'corrected',
    anchorScrolling: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}