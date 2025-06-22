export interface Language {
    id: string,
    authorId: string,
    editorsIds: string[],
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

    function createLanguage(language: { name: string, autoName?: string, autoNameTranscription?: string, isPublished: boolean }) {
        const res = useFetch('/api/v1/languages', {
            method: 'POST',
            body: JSON.stringify(language),
            watch: false,
            headers: { 'Content-Type': 'application/json'  },
            credentials: 'include',
        })
        if (!res.error.value) {
            setTimeout(refresh, 1000)
        }
        return res
    }

    return { data, error, pending, createLanguage, refreshLanguages: refresh }
}