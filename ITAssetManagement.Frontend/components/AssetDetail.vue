<template>
  <div class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50" @click="closeModal">
    <div class="relative top-20 mx-auto p-5 border w-full max-w-4xl shadow-lg rounded-md bg-white" @click.stop>
      <div class="mt-3">
        <!-- Header -->
        <div class="flex items-center justify-between mb-6">
          <div>
            <h3 class="text-lg font-medium text-gray-900">{{ asset.name }}</h3>
            <p class="text-sm text-gray-500">{{ asset.brand }} {{ asset.model }}</p>
          </div>
          <div class="flex items-center space-x-3">
            <span :class="getStatusBadgeClass(asset.status)" class="inline-flex px-3 py-1 text-sm font-semibold rounded-full">
              {{ asset.status }}
            </span>
            <button @click="closeModal" class="text-gray-400 hover:text-gray-600">
              <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
              </svg>
            </button>
          </div>
        </div>

        <!-- Asset Details -->
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <!-- Basic Information -->
          <div class="bg-gray-50 rounded-lg p-4">
            <h4 class="text-sm font-medium text-gray-900 mb-3">Basic Information</h4>
            <dl class="space-y-2">
              <div>
                <dt class="text-xs font-medium text-gray-500">Category</dt>
                <dd class="text-sm text-gray-900">{{ asset.category || '-' }}</dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Serial Number</dt>
                <dd class="text-sm text-gray-900 font-mono">{{ asset.serialNumber || '-' }}</dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Asset Tag</dt>
                <dd class="text-sm text-gray-900 font-mono">{{ asset.assetTag || '-' }}</dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Location</dt>
                <dd class="text-sm text-gray-900">{{ asset.location || '-' }}</dd>
              </div>
            </dl>
          </div>

          <!-- Assignment Information -->
          <div class="bg-gray-50 rounded-lg p-4">
            <h4 class="text-sm font-medium text-gray-900 mb-3">Assignment</h4>
            <dl class="space-y-2">
              <div>
                <dt class="text-xs font-medium text-gray-500">Status</dt>
                <dd class="text-sm text-gray-900">{{ asset.status }}</dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Assigned To</dt>
                <dd class="text-sm text-gray-900">{{ asset.assignedTo || 'Not assigned' }}</dd>
              </div>
              <div v-if="asset.assignedDate">
                <dt class="text-xs font-medium text-gray-500">Assigned Date</dt>
                <dd class="text-sm text-gray-900">{{ formatDate(asset.assignedDate) }}</dd>
              </div>
            </dl>
          </div>

          <!-- Financial Information -->
          <div class="bg-gray-50 rounded-lg p-4">
            <h4 class="text-sm font-medium text-gray-900 mb-3">Financial</h4>
            <dl class="space-y-2">
              <div>
                <dt class="text-xs font-medium text-gray-500">Purchase Date</dt>
                <dd class="text-sm text-gray-900">{{ formatDate(asset.purchaseDate) }}</dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Purchase Price</dt>
                <dd class="text-sm text-gray-900">
                  {{ asset.purchasePrice ? `$${asset.purchasePrice.toFixed(2)}` : '-' }}
                </dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Warranty Expiry</dt>
                <dd class="text-sm text-gray-900">
                  <span :class="getWarrantyStatusClass(asset.warrantyExpiry)">
                    {{ formatDate(asset.warrantyExpiry) }}
                  </span>
                </dd>
              </div>
            </dl>
          </div>

          <!-- System Information -->
          <div class="bg-gray-50 rounded-lg p-4">
            <h4 class="text-sm font-medium text-gray-900 mb-3">System Information</h4>
            <dl class="space-y-2">
              <div>
                <dt class="text-xs font-medium text-gray-500">Created Date</dt>
                <dd class="text-sm text-gray-900">{{ formatDate(asset.createdAt) }}</dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Last Updated</dt>
                <dd class="text-sm text-gray-900">{{ formatDate(asset.updatedAt) }}</dd>
              </div>
              <div>
                <dt class="text-xs font-medium text-gray-500">Created By</dt>
                <dd class="text-sm text-gray-900">{{ asset.createdBy || '-' }}</dd>
              </div>
            </dl>
          </div>

          <!-- Notes -->
          <div v-if="asset.notes" class="bg-gray-50 rounded-lg p-4 md:col-span-2">
            <h4 class="text-sm font-medium text-gray-900 mb-3">Notes</h4>
            <p class="text-sm text-gray-700 whitespace-pre-wrap">{{ asset.notes }}</p>
          </div>
        </div>

        <!-- Action Buttons -->
        <div class="mt-6 flex justify-end space-x-3">
          <button
            @click="closeModal"
            class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Close
          </button>
          <button
            @click="$emit('edit', asset)"
            class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Edit Asset
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  asset: {
    type: Object,
    required: true
  }
})

const emit = defineEmits(['close', 'edit'])

const closeModal = () => {
  emit('close')
}

const getStatusBadgeClass = (status) => {
  const classes = {
    'Available': 'bg-green-100 text-green-800',
    'Assigned': 'bg-blue-100 text-blue-800',
    'Maintenance': 'bg-yellow-100 text-yellow-800',
    'Retired': 'bg-gray-100 text-gray-800'
  }
  return classes[status] || 'bg-gray-100 text-gray-800'
}

const getWarrantyStatusClass = (warrantyExpiry) => {
  if (!warrantyExpiry) return ''
  
  const expiryDate = new Date(warrantyExpiry)
  const today = new Date()
  const thirtyDaysFromNow = new Date(today.getTime() + (30 * 24 * 60 * 60 * 1000))
  
  if (expiryDate < today) {
    return 'text-red-600 font-medium' // Expired
  } else if (expiryDate < thirtyDaysFromNow) {
    return 'text-yellow-600 font-medium' // Expiring soon
  }
  return 'text-green-600' // Valid
}

const formatDate = (dateString) => {
  if (!dateString) return '-'
  return new Date(dateString).toLocaleDateString()
}
</script>
