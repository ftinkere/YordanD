export default defineNuxtRouteMiddleware(async (to, from) => {
    const { isAuthenticated, fetchUser } = useAuth()
    await fetchUser()

    if (! isAuthenticated.value) {
        return navigateTo('/login')
    }
})