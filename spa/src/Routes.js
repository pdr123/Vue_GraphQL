import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/pages/Login/Login.vue';
import ErrorPage from '@/pages/Error/Error';
import Layout from '@/components/Layout/Layout';
// import AnalyticsPage from '@/pages/Dashboard/Dashboard';
import TablesBasicPage from '@/pages/Tables/Basic';

const routes = [
    {
      path: '/login',
      name: 'Login',
      component: Login,
    },
    {
      path: '/error',
      name: 'Error',
      component: ErrorPage,
    },
    {
      path: '/app',
      name: 'Layout',
      component: Layout,
      children: [
        {
          path: 'dashboard',
          name: 'UserResult',
          component: TablesBasicPage,
        },
      ],
    },
  ];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
