import { routes } from "vue-router/auto-routes"

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  pages: true,
  devtools: { enabled: true },
 
  modules: [
    '@nuxtjs/tailwindcss',
    '@pinia/nuxt'
  ],
  css: ['vue3-toastify/dist/index.css'],
  runtimeConfig: {
    apiBase: 'http://localhost:5190/api'
  }
},
)
