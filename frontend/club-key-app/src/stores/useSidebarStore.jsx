import { create } from "zustand";

const useSidebarStore = create((set) => ({
    open: false,
    toggleDrawer: () => set((state) => ({ open: !state.open })),
    closeDrawer: () => set({ open: false }),
    openDrawer: () => set({ open: true }),
}));

export default useSidebarStore;