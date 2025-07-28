<template>
    <div v-if="asset">
      <div class="mb-6 flex justify-between items-center">
        <h1 class="text-3xl font-bold text-gray-900">Asset Details</h1>
        <NuxtLink to="/assets" class="text-blue-600 hover:text-blue-800">
          ← Back to Assets
        </NuxtLink>
      </div>
  
      <div class="bg-white shadow rounded-lg p-6 mb-6">
        <div class="grid grid-cols-2 gap-6">
          <div>
            <h3 class="text-sm font-medium text-gray-500">Asset Tag</h3>
            <p class="mt-1 text-lg font-semibold">{{ asset.assetTag }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Name</h3>
            <p class="mt-1 text-lg">{{ asset.name }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Category</h3>
            <p class="mt-1 text-lg">{{ asset.category }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Status</h3>
            <span :class="getStatusClass(asset.status)" class="mt-1 px-3 py-1 inline-flex text-sm leading-5 font-semibold rounded-full">
              {{ asset.status }}
            </span>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Brand</h3>
            <p class="mt-1 text-lg">{{ asset.brand || '-' }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Model</h3>
            <p class="mt-1 text-lg">{{ asset.model || '-' }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Serial Number</h3>
            <p class="mt-1 text-lg">{{ asset.serialNumber || '-' }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Purchase Date</h3>
            <p class="mt-1 text-lg">{{ formatDate(asset.purchaseDate) }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Purchase Price</h3>
            <p class="mt-1 text-lg">${{ asset.purchasePrice.toFixed(2) }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Assigned To</h3>
            <p class="mt-1 text-lg">{{ asset.assignedTo || '-' }}</p>
          </div>
          
          <div>
            <h3 class="text-sm font-medium text-gray-500">Location</h3>
            <p class="mt-1 text-lg">{{ asset.location || '-' }}</p>
          </div>
          
          <div class="col-span-2">
            <h3 class="text-sm font-medium text-gray-500">Notes</h3>
            <p class="mt-1 text-lg">{{ asset.notes || '-' }}</p>
          </div>
        </div>
      </div>
  
      <!-- Activity Log -->
      <div class="bg-white shadow rounded-lg p-6">
        <h2 class="text-xl font-semibold mb-4">Activity Log</h2>
        <div v-if="activities.length > 0" class="space-y-3">
          <div v-for="activity in activities" :key="activity.id" class="border-l-4 border-gray-200 pl-4 py-2">
            <div class="flex justify-between">
              <p class="text-sm font-medium">{{ activity.action }}</p>
              <p class="text-sm text-gray-500">{{ formatDate(activity.createdAt) }}</p>
            </div>
            <p class="text-sm text-gray-600">{{ activity.details }}</p>
            <p class="text-xs text-gray-500">By: {{ activity.performedBy }}</p>
          </div>
        </div>
        <p v-else class="text-gray-500">No activities recorded</p>
      </div>
    </div>
  </template>
  
  <script setup>
  import dayjs from 'dayjs'
  import { toast } from 'vue3-toastify'
  
  definePageMeta({
    middleware: 'auth'
  })
  
  const { $api } = useNuxtApp()
  const route = useRoute()
  
  const asset = ref(null)
  const activities = ref([])
  
  const getStatusClass = (status) => {
    const classes = {
      'Available': 'bg-green-100 text-green-800',
      'Assigned': 'bg-blue-100 text-blue-800',
      'Repair': 'bg-orange-100 text-orange-800',
      'Retired': 'bg-red-100 text-red-800'
    }
    return classes[status] || 'bg-gray-100 text-gray-800'
  }
  
  const formatDate = (date) => {
    return dayjs(date).format('MMM DD, YYYY')
  }
  
  onMounted(async () => {
    try {
      const assetId = route.params.id
      
      // Load asset details
      const { data: assetData } = await $api.get(`/assets/${assetId}`)
      asset.value = assetData
      
      // Load activities
      const { data: activitiesData } = await $api.get(`/assets/${assetId}/activities`)
      activities.value = activitiesData
    } catch (error) {
      toast.error('Failed to load asset details')
      navigateTo('/assets')
    }
  })
  </script>