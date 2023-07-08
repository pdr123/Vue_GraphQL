# Vue_GraphQL

# Web API Set Up and Testing
1. Install Visual Studio 2022 -> https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false
3. Clone the code and run the WebAPI.
4. Use the following queries to test the WebApi
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

5. 
