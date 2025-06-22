export interface Language {
    id: string,
    authorId: string,
    editorsId: string[],
    name: string,
    autoName: string,
    autoNameTranscription: string,
    description: string,
    isPublished: boolean,
    createdAt: string,
}

export const useLanguages = () => {
    const { data, error, pending, refresh } = useFetch('/api/v1/languages', {
        credentials: 'include',
    })

    async function createLanguage(language: { name: string, autoName?: string, autoNameTranscription?: string, isPublished: boolean }) {
        const { error } = await useFetch('/api/v1/languages', {
            method: 'POST',
            body: JSON.stringify(language),
            watch: false,
            headers: { 'Content-Type': 'application/json'  },
            credentials: 'include',
        })
        if (!error.value) {
            await refresh()
            return true
        }
        return false
    }

    return { data, error, pending, createLanguage, refreshLanguages: refresh }
}