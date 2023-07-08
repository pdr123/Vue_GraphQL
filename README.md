# Vue_GraphQL

# Web API Set Up and Testing
1. [Install Visual Studio 2022](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false)
2. To create a local SqlServer Instance in windows [Install Sql Server 2022](https://go.microsoft.com/fwlink/p/?linkid=2215158&clcid=0x409&culture=en-us&country=us)
3. Run the [Scripts](https://github.com/pdr123/Vue_GraphQL/blob/main/Scripts/Create_Data_StoreDB.sql.txt) to create test data.
4. Clone the code and run the WebAPI.
5. Use the following queries to test the WebApi
   ```
   query {
    resultsByUser(userId: 1) {
        userId,
        noOfAttempts,
        user {
        lastName,
        email,
        dateOfBirth
        },
        activity {
          activityId,
          name,
          description
        },
        status {
          status
        }
    }
   }
   
    #query {
    #     allResults {
    #         resultId,
    #         noOfAttempts,
    #         durationHours,
    #         durationMinutes,
    #         startDate,
    #         endDate,
    #         userId,
    #         statusId,
    #         status {
    #           id,
    #           status,
    #           description
    #         }
    #         # user {
    #         # lastName,
    #         # email,
    #         # dateOfBirth
    #         # },
    #         activity {
    #           activityId,
    #           name,
    #           description,
    #           inchargeId,
    #           user {
    #             userId,
    #             firstName
    #           }
    #         }
    #     }
    # }
   ```
   # SPA (Vue) SetUp and Test
Based on https://www.apollographql.com/blog/frontend/getting-started-with-vue-apollo/

1) [Install latest version of Node](https://nodejs.org/en/download) - Node v18.16.1
2) Npm v9.8.0
3) Vue CLI v5.0.8

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).
