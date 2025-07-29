<template>
  <form @submit.prevent="handleSubmit">
    <div class="grid grid-cols-2 gap-4">
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Asset Tag</label>
        <input
          v-model="form.assetTag"
          type="text"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Name</label>
        <input
          v-model="form.name"
          type="text"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Category</label>
        <select
          v-model="form.category"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        >
          <option value="">Select Category</option>
          <option value="Laptop">Laptop</option>
          <option value="Desktop">Desktop</option>
          <option value="Monitor">Monitor</option>
          <option value="Printer">Printer</option>
          <option value="Network">Network</option>
          <option value="Mobile">Mobile</option>
          <option value="Other">Other</option>
        </select>
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Brand</label>
        <input
          v-model="form.brand"
          type="text"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Model</label>
        <input
          v-model="form.model"
          type="text"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Serial Number</label>
        <input
          v-model="form.serialNumber"
          type="text"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Purchase Date</label>
        <input
          v-model="form.purchaseDate"
          type="date"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Purchase Price</label>
        <input
          v-model.number="form.purchasePrice"
          type="number"
          step="0.01"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Status</label>
        <select
          v-model="form.status"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        >
          <option value="Available">Available</option>
          <option value="Assigned">Assigned</option>
          <option value="Repair">Repair</option>
          <option value="Retired">Retired</option>
        </select>
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Assigned To</label>
        <input
          v-model="form.assignedTo"
          type="text"
          :disabled="form.status !== 'Assigned'"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500 disabled:bg-gray-100"
        />
      </div>
      
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Location</label>
        <input
          v-model="form.location"
          type="text"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      
      <div class="col-span-2">
        <label class="block text-sm font-medium text-gray-700 mb-1">Notes</label>
        <textarea
          v-model="form.notes"
          rows="3"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        ></textarea>
      </div>
    </div>
    
    <div class="mt-6 flex justify-end space-x-3">
      <button
        type="button"
        @click="$emit('cancel')"
        class="px-4 py-2 border border-gray-300 rounded-md text-gray-700 hover:bg-gray-50"
      >
        Cancel
      </button>
      <button
        type="submit"
        class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
      >
        {{ asset ? 'Update' : 'Create' }} Asset
      </button>
    </div>
  </form>
</template>

<script setup>
import dayjs from 'dayjs'

const props = defineProps({
  asset: {
    type: Object,
    default: null
  }
})

const emit = defineEmits(['save', 'cancel'])

const form = ref({
  assetTag: '',
  name: '',
  category: '',
  brand: '',
  model: '',
  serialNumber: '',
  purchaseDate: '',
  purchasePrice: 0,
  status: 'Available',
  assignedTo: '',
  location: '',
  notes: ''
})

onMounted(() => {
  if (props.asset) {
    form.value = {
      ...props.asset,
      purchaseDate: dayjs(props.asset.purchaseDate).format('YYYY-MM-DD')
    }
  }
})

watch(() => form.value.status, (newStatus) => {
  if (newStatus !== 'Assigned') {
    form.value.assignedTo = ''
    form.value.assignedDate = null
  } else if (!form.value.assignedDate) {
    form.value.assignedDate = new Date().toISOString()
  }
})

const handleSubmit = () => {
  const data = {
    ...form.value,
    purchaseDate: new Date(form.value.purchaseDate).toISOString()
  }
  
  if (data.status === 'Assigned' && data.assignedTo && !data.assignedDate) {
    data.assignedDate = new Date().toISOString()
  }
  
  emit('save', data)
}
</script>
