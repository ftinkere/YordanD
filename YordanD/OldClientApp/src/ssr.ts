import { createSSRApp, type DefineComponent, h } from 'vue'
import { createInertiaApp } from '@inertiajs/vue3'
import { renderToString } from '@vue/server-renderer'
import './assets/main.css'
import createServer from '@inertiajs/vue3/server'

createServer(page => 
    createInertiaApp({
        page,
        render: renderToString,
        resolve: name => {
            const pages = import.meta.glob('./Pages/**/*.vue', { eager: true })
            let page = pages[`./Pages/${name}.vue`] as DefineComponent|undefined
            if (! page) {
                throw new Error(`Page [${name}] not found.`)
            }
    
            return page
        },
        setup({ App, props, plugin }) {
            createSSRApp({ render: () => h(App, props) })
                .use(plugin)
        },
    })
)