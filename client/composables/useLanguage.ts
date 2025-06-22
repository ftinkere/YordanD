export const useLanguage = (id: string) => {
    const { data, error, pending, refresh } = useFetch<Language>('/api/v1/languages/' + id, {
        credentials: 'include',
        watch: false,
    })

    return { data, error, pending, refresh }
}