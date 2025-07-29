<template>
  <div class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50" @click="closeModal">
    <div class="relative top-20 mx-auto p-5 border w-full max-w-2xl shadow-lg rounded-md bg-white" @click.stop>
      <div class="mt-3">
        <!-- Header -->
        <div class="flex items-center justify-between mb-6">
          <h3 class="text-lg font-medium text-gray-900">
            {{ isEdit ? 'Edit Asset' : 'Add New Asset' }}
          </h3>
          <button @click="closeModal" class="text-gray-400 hover:text-gray-600">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
            </svg>
          </button>
        </div>

        <!-- Form -->
        <form @submit.prevent="submitForm">
          <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
            <!-- Asset Name -->
            <div>
              <label for="name" class="block text-sm font-medium text-gray-700 mb-1">
                Asset Name *
              </label>
              <input
                id="name"
                v-model="form.name"
                type="text"
                required
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Enter asset name"
              />
            </div>

            <!-- Category -->
            <div>
              <label for="category" class="block text-sm font-medium text-gray-700 mb-1">
                Category *
              </label>
              <select
                id="category"
                v-model="form.category"
                required
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              >
                <option value="">Select category</option>
                <option value="Laptop">Laptop</option>
                <option value="Desktop">Desktop</option>
                <option value="Monitor">Monitor</option>
                <option value="Phone">Phone</option>
                <option value="Tablet">Tablet</option>
                <option value="Server">Server</option>
                <option value="Network">Network</option>
                <option value="Printer">Printer</option>
                <option value="Other">Other</option>
              </select>
            </div>

            <!-- Brand -->
            <div>
              <label for="brand" class="block text-sm font-medium text-gray-700 mb-1">
                Brand
              </label>
              <input
                id="brand"
                v-model="form.brand"
                type="text"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Enter brand"
              />
            </div>

            <!-- Model -->
            <div>
              <label for="model" class="block text-sm font-medium text-gray-700 mb-1">
                Model
              </label>
              <input
                id="model"
                v-model="form.model"
                type="text"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Enter model"
              />
            </div>

            <!-- Serial Number -->
            <div>
              <label for="serialNumber" class="block text-sm font-medium text-gray-700 mb-1">
                Serial Number *
              </label>
              <input
                id="serialNumber"
                v-model="form.serialNumber"
                type="text"
                required
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Enter serial number"
              />
            </div>

            <!-- Asset Tag -->
            <div>
              <label for="assetTag" class="block text-sm font-medium text-gray-700 mb-1">
                Asset Tag
              </label>
              <input
                id="assetTag"
                v-model="form.assetTag"
                type="text"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Enter asset tag"
              />
            </div>

            <!-- Status -->
            <div>
              <label for="status" class="block text-sm font-medium text-gray-700 mb-1">
                Status *
              </label>
              <select
                id="status"
                v-model="form.status"
                required
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              >
                <option value="">Select status</option>
                <option value="Available">Available</option>
                <option value="Assigned">Assigned</option>
                <option value="Maintenance">Maintenance</option>
                <option value="Retired">Retired</option>
              </select>
            </div>

            <!-- Assigned To -->
            <div>
              <label for="assignedTo" class="block text-sm font-medium text-gray-700 mb-1">
                Assigned To
              </label>
              <input
                id="assignedTo"
                v-model="form.assignedTo"
                type="text"
                :disabled="form.status !== 'Assigned'"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:bg-gray-100"
                placeholder="Enter employee name"
              />
            </div>

            <!-- Purchase Date -->
            <div>
              <label for="purchaseDate" class="block text-sm font-medium text-gray-700 mb-1">
                Purchase Date
              </label>
              <input
                id="purchaseDate"
                v-model="form.purchaseDate"
                type="date"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>

            <!-- Purchase Price -->
            <div>
              <label for="purchasePrice" class="block text-sm font-medium text-gray-700 mb-1">
                Purchase Price
              </label>
              <input
                id="purchasePrice"
                v-model="form.purchasePrice"
                type="number"
                step="0.01"
                min="0"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="0.00"
              />
            </div>

            <!-- Warranty Expiry -->
            <div>
              <label for="warrantyExpiry" class="block text-sm font-medium text-gray-700 mb-1">
                Warranty Expiry
              </label>
              <input
                id="warrantyExpiry"
                v-model="form.warrantyExpiry"
                type="date"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>

            <!-- Location -->
            <div>
              <label for="location" class="block text-sm font-medium text-gray-700 mb-1">
                Location
              </label>
              <input
                id="location"
                v-model="form.location"
                type="text"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Enter location"
              />
            </div>
          </div>

          <!-- Notes -->
          <div class="mt-6">
            <label for="notes" class="block text-sm font-medium text-gray-700 mb-1">
              Notes
            </label>
            <textarea
              id="notes"
              v-model="form.notes"
              rows="3"
              class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              placeholder="Enter any additional notes..."
            ></textarea>
          </div>

          <!-- Form Actions -->
          <div class="mt-6 flex justify-end space-x-3">
            <button
              type="button"
              @click="closeModal"
              class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
            >
              Cancel
            </button>
            <button
              type="submit"
              :disabled="loading"
              class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50"
            >
              {{ loading ? 'Saving...' : (isEdit ? 'Update Asset' : 'Create Asset') }}
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  asset: {
    type: Object,
    default: null
  },
  loading: {
    type: Boolean,
    default: false
  }
})

const emit = defineEmits(['close', 'submit'])

const isEdit = computed(() => !!props.asset?.id)

const form = ref({
  name: '',
  category: '',
  brand: '',
  model: '',
  serialNumber: '',
  assetTag: '',
  status: 'Available',
  assignedTo: '',
  purchaseDate: '',
  purchasePrice: null,
  warrantyExpiry: '',
  location: '',
  notes: ''
})

// Initialize form with asset data if editing
watch(() => props.asset, (newAsset) => {
  if (newAsset) {
    Object.keys(form.value).forEach(key => {
      if (newAsset[key] !== undefined) {
        // Format dates for input[type="date"]
        if ((key === 'purchaseDate' || key === 'warrantyExpiry') && newAsset[key]) {
          form.value[key] = new Date(newAsset[key]).toISOString().split('T')[0]
        } else {
          form.value[key] = newAsset[key]
        }
      }
    })
  }
}, { immediate: true })

// Clear assignedTo when status is not "Assigned"
watch(() => form.value.status, (newStatus) => {
  if (newStatus !== 'Assigned') {
    form.value.assignedTo = ''
  }
})

const closeModal = () => {
  emit('close')
}

const submitForm = () => {
  const formData = { ...form.value }
  
  // Convert empty strings to null for optional fields
  Object.keys(formData).forEach(key => {
    if (formData[key] === '') {
      formData[key] = null
    }
  })

  // Add ID if editing
  if (isEdit.value) {
    formData.id = props.asset.id
  }

  emit('submit', formData)
}

// Reset form when modal closes
watch(() => props.asset, (newAsset) => {
  if (!newAsset) {
    // Reset form for new asset
    form.value = {
      name: '',
      category: '',
      brand: '',
      model: '',
      serialNumber: '',
      assetTag: '',
      status: 'Available',
      assignedTo: '',
      purchaseDate: '',
      purchasePrice: null,
      warrantyExpiry: '',
      location: '',
      notes: ''
    }
  }
})
</script>
