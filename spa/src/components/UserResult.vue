<template>
  <div>
    <h1>{{ msg }}</h1>
  </div>
  <p v-if="error">Something went wrong...</p>
  <p v-else-if="loading">Loading...</p>
  <p v-else-if="result == null">Api wrong</p>
  <p v-else v-for="result in result.allResults" :key="result.resultId">
    {{ result.user.firstName }} - {{result.noOfAttempts}} - {{result.activity.name}}
  </p>
</template>

<script>

import gql from 'graphql-tag';
import { useQuery } from '@vue/apollo-composable'

const CHARACTERS_QUERY = gql`
  query {
          allResults {
            resultId,
            user {
              firstName,
              dateOfBirth
            },
            activity {
              name,
              inchargeId
            }
            noOfAttempts,
            status {
              description
            }
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

