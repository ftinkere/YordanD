import { defineStore } from 'pinia'
import { ref } from "vue";
import type { User } from "@/types";

export const useUserStore = defineStore('user', () => {
    const user = ref<User|null>(null)
    function setUser(newUser: User|null) {
        user.value = newUser
    }
    return { user, setUser }
})