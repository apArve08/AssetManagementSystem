<template>
  <div class="bg-white shadow-lg rounded-lg overflow-hidden">
    <!-- Search and Filter Bar -->
    <div class="bg-gray-50 px-6 py-4 border-b">
      <div class="flex flex-col sm:flex-row gap-4">
        <div class="flex-1">
          <input
            v-model="searchTerm"
            type="text"
            placeholder="Search assets..."
            class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
        <div class="flex gap-2">
          <select
            v-model="statusFilter"
            class="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="">All Status</option>
            <option value="Available">Available</option>
            <option value="Assigned">Assigned</option>
            <option value="Maintenance">Maintenance</option>
            <option value="Retired">Retired</option>
          </select>
          <select
            v-model="categoryFilter"
            class="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="">All Categories</option>
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
      </div>
    </div>

    <!-- Assets Table -->
    <div v-if="loading" class="p-8 text-center">
      <div class="inline-block animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
      <p class="mt-2 text-gray-600">Loading assets...</p>
    </div>

    <div v-else-if="filteredAssets.length === 0" class="p-8 text-center text-gray-500">
      <svg class="mx-auto h-12 w-12 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5H7a2 2 0 00-2 2v10a2 2 0 002 2h8a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
      </svg>
      <h3 class="mt-2 text-sm font-medium text-gray-900">No assets found</h3>
      <p class="mt-1 text-sm text-gray-500">Get started by adding your first asset.</p>
    </div>

    <div v-else class="overflow-x-auto">
      <table class="min-w-full divide-y divide-gray-200">
        <thead class="bg-gray-50">
          <tr>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Asset</th>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Category</th>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Serial Number</th>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Status</th>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Assigned To</th>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Purchase Date</th>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Actions</th>
          </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
          <tr v-for="asset in paginatedAssets" :key="asset.id" class="hover:bg-gray-50">
            <td class="px-6 py-4 whitespace-nowrap">
              <div>
                <div class="text-sm font-medium text-gray-900">{{ asset.name }}</div>
                <div class="text-sm text-gray-500">{{ asset.brand }} {{ asset.model }}</div>
              </div>
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ asset.category }}</td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ asset.serialNumber }}</td>
            <td class="px-6 py-4 whitespace-nowrap">
              <span :class="getStatusBadgeClass(asset.status)" class="inline-flex px-2 py-1 text-xs font-semibold rounded-full">
                {{ asset.status }}
              </span>
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
              {{ asset.assignedTo || '-' }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
              {{ formatDate(asset.purchaseDate) }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
              <div class="flex space-x-2">
                <button
                  @click="$emit('view', asset)"
                  class="text-blue-600 hover:text-blue-900"
                >
                  View
                </button>
                <button
                  @click="$emit('edit', asset)"
                  class="text-indigo-600 hover:text-indigo-900"
                >
                  Edit
                </button>
                <button
                  @click="$emit('delete', asset)"
                  class="text-red-600 hover:text-red-900"
                >
                  Delete
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Pagination -->
    <div v-if="totalPages > 1" class="bg-white px-4 py-3 flex items-center justify-between border-t border-gray-200 sm:px-6">
      <div class="flex-1 flex justify-between sm:hidden">
        <button
          @click="currentPage > 1 && (currentPage--)"
          :disabled="currentPage === 1"
          class="relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 disabled:opacity-50"
        >
          Previous
        </button>
        <button
          @click="currentPage < totalPages && (currentPage++)"
          :disabled="currentPage === totalPages"
          class="ml-3 relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 disabled:opacity-50"
        >
          Next
        </button>
      </div>
      <div class="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
        <div>
          <p class="text-sm text-gray-700">
            Showing {{ ((currentPage - 1) * itemsPerPage) + 1 }} to {{ Math.min(currentPage * itemsPerPage, filteredAssets.length) }} of {{ filteredAssets.length }} results
          </p>
        </div>
        <div>
          <nav class="relative z-0 inline-flex rounded-md shadow-sm -space-x-px">
            <button
              @click="currentPage > 1 && (currentPage--)"
              :disabled="currentPage === 1"
              class="relative inline-flex items-center px-2 py-2 rounded-l-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 disabled:opacity-50"
            >
              Previous
            </button>
            <button
              v-for="page in visiblePages"
              :key="page"
              @click="currentPage = page"
              :class="[
                page === currentPage
                  ? 'z-10 bg-blue-50 border-blue-500 text-blue-600'
                  : 'bg-white border-gray-300 text-gray-500 hover:bg-gray-50',
                'relative inline-flex items-center px-4 py-2 border text-sm font-medium'
              ]"
            >
              {{ page }}
            </button>
            <button
              @click="currentPage < totalPages && (currentPage++)"
              :disabled="currentPage === totalPages"
              class="relative inline-flex items-center px-2 py-2 rounded-r-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 disabled:opacity-50"
            >
              Next
            </button>
          </nav>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  assets: {
    type: Array,
    default: () => []
  },
  loading: {
    type: Boolean,
    default: false
  }
})

defineEmits(['view', 'edit', 'delete'])

const searchTerm = ref('')
const statusFilter = ref('')
const categoryFilter = ref('')
const currentPage = ref(1)
const itemsPerPage = 10

const filteredAssets = computed(() => {
  let filtered = props.assets

  if (searchTerm.value) {
    const search = searchTerm.value.toLowerCase()
    filtered = filtered.filter(asset =>
      asset.name.toLowerCase().includes(search) ||
      asset.brand.toLowerCase().includes(search) ||
      asset.model.toLowerCase().includes(search) ||
      asset.serialNumber.toLowerCase().includes(search) ||
      asset.assignedTo?.toLowerCase().includes(search)
    )
  }

  if (statusFilter.value) {
    filtered = filtered.filter(asset => asset.status === statusFilter.value)
  }

  if (categoryFilter.value) {
    filtered = filtered.filter(asset => asset.category === categoryFilter.value)
  }

  return filtered
})

const totalPages = computed(() => Math.ceil(filteredAssets.value.length / itemsPerPage))

const paginatedAssets = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  const end = start + itemsPerPage
  return filteredAssets.value.slice(start, end)
})

const visiblePages = computed(() => {
  const pages = []
  const start = Math.max(1, currentPage.value - 2)
  const end = Math.min(totalPages.value, currentPage.value + 2)
  
  for (let i = start; i <= end; i++) {
    pages.push(i)
  }
  
  return pages
})

const getStatusBadgeClass = (status) => {
  const classes = {
    'Available': 'bg-green-100 text-green-800',
    'Assigned': 'bg-blue-100 text-blue-800',
    'Maintenance': 'bg-yellow-100 text-yellow-800',
    'Retired': 'bg-gray-100 text-gray-800'
  }
  return classes[status] || 'bg-gray-100 text-gray-800'
}

const formatDate = (dateString) => {
  if (!dateString) return '-'
  return new Date(dateString).toLocaleDateString()
}

// Reset pagination when filters change
watch([searchTerm, statusFilter, categoryFilter], () => {
  currentPage.value = 1
})
</script>
