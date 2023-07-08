<template>
  <div>
    <h1>{{ msg }}</h1>
  </div>
  <p v-if="error">Something went wrong...</p>
  <p v-else-if="loading">Loading...</p>
  <p v-else-if="result == null">Api wrong</p>
  <p v-else v-for="product in result.allProducts" :key="product.productId">
    {{ product.productName }}
  </p>
</template>

<script>

import gql from 'graphql-tag';
import { useQuery } from '@vue/apollo-composable'

const CHARACTERS_QUERY = gql`
  query {
          allProducts {
            productId, 
            productName
          }
        }`

export default {
  name: 'UserResult',
  setup () {
    const {result, loading, error} = useQuery(CHARACTERS_QUERY);
    return {result, loading, error}
  },
  props: {
    msg : String
  }
}
</script>

