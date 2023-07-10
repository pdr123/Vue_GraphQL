// https://www.apollographql.com/blog/frontend/getting-started-with-vue-apollo/

import { ApolloClient, InMemoryCache } from '@apollo/client/core'
import { createApp,provide, h } from 'vue'
import { DefaultApolloClient, provideApolloClient } from '@vue/apollo-composable'
import App from './App.vue'
import router from './Routes'
import store from './store'
import layoutMixin from './mixins/layout';
// import VueTouch from 'vue-touch';
// import Toasted from 'vue-toasted';
// import * as VueGoogleMaps from 'vue2-google-maps';
import Widget from './components/Widget/Widget';

const cache = new InMemoryCache()

const apolloClient = new ApolloClient({
    cache,
    uri: 'http://localhost:5244/graphql',
  })

  provideApolloClient(apolloClient);

  const app = createApp({
    setup () {
      provide(DefaultApolloClient, apolloClient)
    },
  
    render: () => h(App),
  })

  // app.use(VueTouch);
  app.component('AppWidget', Widget);
  // app.use(VueGoogleMaps, {
  //   load: {
  //     key: 'AIzaSyB7OXmzfQYua_1LEhRdqsoYzyJOPh9hGLg',
  //   },
  // });
  app.mixin(layoutMixin)
  // app.use(Toasted, {duration: 10000});
  app.use(router)
  app.use(store)

  app.mount('#app');
