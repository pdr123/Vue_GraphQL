// https://www.apollographql.com/blog/frontend/getting-started-with-vue-apollo/

import { ApolloClient, InMemoryCache } from '@apollo/client/core'
import { createApp, provide, h } from 'vue'
import { DefaultApolloClient } from '@vue/apollo-composable'
import App from './App.vue'

const cache = new InMemoryCache()

const apolloClient = new ApolloClient({
    cache,
    uri: 'http://localhost:5244/graphql',
  })

  const app = createApp({
    setup () {
      provide(DefaultApolloClient, apolloClient)
    },
  
    render: () => h(App),
  })

  app.mount('#app');