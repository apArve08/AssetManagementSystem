import { defineNuxtPlugin, useRouter } from "nuxt/app"

export default defineNuxtPlugin(() => {
    const router = useRouter()
    console.log('Nuxt routes:', router.getRoutes().map(r => r.path))
  })