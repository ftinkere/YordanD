import { createApp, type DefineComponent, h } from 'vue'
import { createInertiaApp } from '@inertiajs/vue3'
import './assets/main.css'
import { createPinia } from 'pinia'
import { createVfm } from 'vue-final-modal'
import 'vue-final-modal/style.css'


createInertiaApp({
    id: 'app',
    resolve: name => {
        const pages = import.meta.glob('./Pages/**/*.vue', { eager: true })
        let page = pages[`./Pages/${name}.vue`] as DefineComponent|undefined
        if (! page) {
            throw new Error(`Page [${name}] not found.`)
        }

        return page
    },
    setup({ el, App, props, plugin }) {
        const pinia = createPinia()
        const vfm = createVfm()

        createApp({ render: () => h(App, props) })
            .use(plugin)
            .use(pinia)
            .use(vfm)
            .mount(el)
    },
})