import { create } from 'zustand';

const useErrorStore = create((set) => ({
    error: null,
    setError: (message) => set({ error: message }),
    clearError: () => set({ error: null })
}));

export default useErrorStore;