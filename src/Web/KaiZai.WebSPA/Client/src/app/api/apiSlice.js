import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const apiSlice = createApi({
    reducerPath: 'api', // optional
    baseQuery: fetchBaseQuery({
        // eslint-disable-next-line no-undef
        baseUrl: process.env.REACT_APP_API_URL,
        prepareHeaders: (headers) => {
            headers.set('ProfileId', 'a476e83e-3ecc-4708-8880-af88c4dd04da')
            return headers
        }
    }),
    tagTypes: ['Incomes'],
    // eslint-disable-next-line no-unused-vars
    endpoints: builder => ({})
})