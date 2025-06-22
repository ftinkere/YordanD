import { cva, type VariantProps } from 'class-variance-authority'
import { cn } from "@/lib/utils.ts";

const baseClass = 'inline-flex items-center justify-center gap-2 whitespace-nowrap shadow-xs rounded-sm text-sm font-medium cursor-pointer transition-all disabled:pointer-events-none disabled:opacity-50 [&_svg]:pointer-events-none [&_svg:not([class*=\'size-\'])]:size-4 shrink-0 [&_svg]:shrink-0 outline-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px] aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive';

const sizes = {
  xs: 'h-6  px-2 text-xs   has-[>svg]:px-2',
  sm: 'h-8  px-3 text-sm   has-[>svg]:px-2.5',
  md: 'h-9  px-4 text-base has-[>svg]:px-3',
  lg: 'h-10 px-5 text-lg   has-[>svg]:px-4',
  xl: 'h-12 px-6 text-xl   has-[>svg]:px-4',
};

export const solidButtonVariants = cva(
    baseClass,
    {
      variants: {
        size: sizes,
        color: {
          zinc:    'bg-zinc-500    text-white hover:bg-zinc-600    dark:bg-zinc-600    dark:text-white    dark:hover:bg-zinc-500    focus-visible:ring-zinc-500/50',
          red:     'bg-red-500     text-white hover:bg-red-600     dark:bg-red-600     dark:text-white    dark:hover:bg-red-500     focus-visible:ring-red-500/50',
          orange:  'bg-orange-500  text-white hover:bg-orange-600  dark:bg-orange-600  dark:text-white    dark:hover:bg-orange-500  focus-visible:ring-orange-500/50',
          amber:   'bg-amber-500   text-white hover:bg-amber-600   dark:bg-amber-600   dark:text-zinc-950 dark:hover:bg-amber       focus-visible:ring-amber-500/50-500',
          yellow:  'bg-yellow-500  text-white hover:bg-yellow-600  dark:bg-yellow-400  dark:text-zinc-950 dark:hover:bg-yellow      focus-visible:ring-yellow-500/50-300',
          lime:    'bg-lime-500    text-white hover:bg-lime-600    dark:bg-lime-600    dark:text-white    dark:hover:bg-lime-500    focus-visible:ring-lime-500/50',
          green:   'bg-green-500   text-white hover:bg-green-600   dark:bg-green-600   dark:text-white    dark:hover:bg-green-500   focus-visible:ring-green-500/50',
          emerald: 'bg-emerald-500 text-white hover:bg-emerald-600 dark:bg-emerald-600 dark:text-white    dark:hover:bg-emerald-500 focus-visible:ring-emerald-500/50',
          teal:    'bg-teal-500    text-white hover:bg-teal-600    dark:bg-teal-600    dark:text-white    dark:hover:bg-teal-500    focus-visible:ring-teal-500/50',
          cyan:    'bg-cyan-500    text-white hover:bg-cyan-600    dark:bg-cyan-600    dark:text-white    dark:hover:bg-cyan-500    focus-visible:ring-cyan-500/50',
          sky:     'bg-sky-500     text-white hover:bg-sky-600     dark:bg-sky-600     dark:text-white    dark:hover:bg-sky-500     focus-visible:ring-sky-500/50',
          blue:    'bg-blue-500    text-white hover:bg-blue-600    dark:bg-blue-600    dark:text-white    dark:hover:bg-blue-500    focus-visible:ring-blue-500/50',
          indigo:  'bg-indigo-500  text-white hover:bg-indigo-600  dark:bg-indigo-600  dark:text-white    dark:hover:bg-indigo-500  focus-visible:ring-indigo-500/50',
          violet:  'bg-violet-500  text-white hover:bg-violet-600  dark:bg-violet-600  dark:text-white    dark:hover:bg-violet-500  focus-visible:ring-violet-500/50',
          purple:  'bg-purple-500  text-white hover:bg-purple-600  dark:bg-purple-600  dark:text-white    dark:hover:bg-purple-500  focus-visible:ring-purple-500/50',
          fuchsia: 'bg-fuchsia-500 text-white hover:bg-fuchsia-600 dark:bg-fuchsia-600 dark:text-white    dark:hover:bg-fuchsia-500 focus-visible:ring-fuchsia-500/50',
          pink:    'bg-pink-500    text-white hover:bg-pink-600    dark:bg-pink-600    dark:text-white    dark:hover:bg-pink-500    focus-visible:ring-pink-500/50',
          rose:    'bg-rose-500    text-white hover:bg-rose-600    dark:bg-rose-600    dark:text-white    dark:hover:bg-rose-500    focus-visible:ring-rose-500/50',
        },
      },
      defaultVariants: {
        size: 'md',
        color: 'zinc',
      },
    }
)
export type SolidButtonVariants = VariantProps<typeof solidButtonVariants>

export const lightButtonVariants = cva(
    baseClass,
    {
      variants: {
        size: sizes,
        color: {
          zinc:    'bg-zinc-500/20    text-zinc-500    hover:bg-zinc-500/30    dark:bg-zinc-500/30    dark:text-zinc-200    dark:hover:bg-zinc-500/40    focus-visible:ring-zinc-500/50',
          red:     'bg-red-500/20     text-red-500     hover:bg-red-500/30     dark:bg-red-500/30     dark:text-red-200     dark:hover:bg-red-500/40     focus-visible:ring-red-500/50',
          orange:  'bg-orange-500/20  text-orange-500  hover:bg-orange-500/30  dark:bg-orange-500/30  dark:text-orange-200  dark:hover:bg-orange-500/40  focus-visible:ring-orange-500/50',
          amber:   'bg-amber-500/20   text-amber-500   hover:bg-amber-500/30   dark:bg-amber-500/30   dark:text-amber-200   dark:hover:bg-amber-500/40   focus-visible:ring-amber-500/50',
          yellow:  'bg-yellow-500/20  text-yellow-500  hover:bg-yellow-500/30  dark:bg-yellow-500/30  dark:text-yellow-200  dark:hover:bg-yellow-500/40  focus-visible:ring-yellow-500/50',
          lime:    'bg-lime-500/20    text-lime-500    hover:bg-lime-500/30    dark:bg-lime-500/30    dark:text-lime-200    dark:hover:bg-lime-500/40    focus-visible:ring-lime-500/50',
          green:   'bg-green-500/20   text-green-500   hover:bg-green-500/30   dark:bg-green-500/30   dark:text-green-200   dark:hover:bg-green-500/40   focus-visible:ring-green-500/50',
          emerald: 'bg-emerald-500/20 text-emerald-500 hover:bg-emerald-500/30 dark:bg-emerald-500/30 dark:text-emerald-200 dark:hover:bg-emerald-500/40 focus-visible:ring-emerald-500/50',
          teal:    'bg-teal-500/20    text-teal-500    hover:bg-teal-500/30    dark:bg-teal-500/30    dark:text-teal-200    dark:hover:bg-teal-500/40    focus-visible:ring-teal-500/50',
          cyan:    'bg-cyan-500/20    text-cyan-500    hover:bg-cyan-500/30    dark:bg-cyan-500/30    dark:text-cyan-200    dark:hover:bg-cyan-500/40    focus-visible:ring-cyan-500/50',
          sky:     'bg-sky-500/20     text-sky-500     hover:bg-sky-500/30     dark:bg-sky-500/30     dark:text-sky-200     dark:hover:bg-sky-500/40     focus-visible:ring-sky-500/50',
          blue:    'bg-blue-500/20    text-blue-500    hover:bg-blue-500/30    dark:bg-blue-500/30    dark:text-blue-200    dark:hover:bg-blue-500/40    focus-visible:ring-blue-500/50',
          indigo:  'bg-indigo-500/20  text-indigo-500  hover:bg-indigo-500/30  dark:bg-indigo-500/30  dark:text-indigo-200  dark:hover:bg-indigo-500/40  focus-visible:ring-indigo-500/50',
          violet:  'bg-violet-500/20  text-violet-500  hover:bg-violet-500/30  dark:bg-violet-500/30  dark:text-violet-200  dark:hover:bg-violet-500/40  focus-visible:ring-violet-500/50',
          purple:  'bg-purple-500/20  text-purple-500  hover:bg-purple-500/30  dark:bg-purple-500/30  dark:text-purple-200  dark:hover:bg-purple-500/40  focus-visible:ring-purple-500/50',
          fuchsia: 'bg-fuchsia-500/20 text-fuchsia-500 hover:bg-fuchsia-500/30 dark:bg-fuchsia-500/30 dark:text-fuchsia-200 dark:hover:bg-fuchsia-500/40 focus-visible:ring-fuchsia-500/50',
          pink:    'bg-pink-500/20    text-pink-500    hover:bg-pink-500/30    dark:bg-pink-500/30    dark:text-pink-200    dark:hover:bg-pink-500/40    focus-visible:ring-pink-500/50',
          rose:    'bg-rose-500/20    text-rose-500    hover:bg-rose-500/30    dark:bg-rose-500/30    dark:text-rose-200    dark:hover:bg-rose-500/40    focus-visible:ring-rose-500/50',
        }
      },
      defaultVariants: {
        size: 'md',
        color: 'zinc',
      },
    }
)
export type LightButtonVariants = VariantProps<typeof lightButtonVariants>

export const ghostButtonVariants = cva(
    cn(baseClass, 'shadow-none'),
    {
      variants: {
        size: sizes,
        color: {
          zinc:    'text-zinc-500    hover:bg-zinc-500/10    dark:text-zinc-200    dark:hover:bg-zinc-500/20',
          red:     'text-red-500     hover:bg-red-500/10     dark:text-red-200     dark:hover:bg-red-500/20',
          orange:  'text-orange-500  hover:bg-orange-500/10  dark:text-orange-200  dark:hover:bg-orange-500/20',
          amber:   'text-amber-500   hover:bg-amber-500/10   dark:text-amber-200   dark:hover:bg-amber-500/20',
          yellow:  'text-yellow-500  hover:bg-yellow-500/10  dark:text-yellow-200  dark:hover:bg-yellow-500/20',
          lime:    'text-lime-500    hover:bg-lime-500/10    dark:text-lime-200    dark:hover:bg-lime-500/20',
          green:   'text-green-500   hover:bg-green-500/10   dark:text-green-200   dark:hover:bg-green-500/20',
          emerald: 'text-emerald-500 hover:bg-emerald-500/10 dark:text-emerald-200 dark:hover:bg-emerald-500/20',
          teal:    'text-teal-500    hover:bg-teal-500/10    dark:text-teal-200    dark:hover:bg-teal-500/20',
          cyan:    'text-cyan-500    hover:bg-cyan-500/10    dark:text-cyan-200    dark:hover:bg-cyan-500/20',
          sky:     'text-sky-500     hover:bg-sky-500/10     dark:text-sky-200     dark:hover:bg-sky-500/20',
          blue:    'text-blue-500    hover:bg-blue-500/10    dark:text-blue-200    dark:hover:bg-blue-500/20',
          indigo:  'text-indigo-500  hover:bg-indigo-500/10  dark:text-indigo-200  dark:hover:bg-indigo-500/20',
          violet:  'text-violet-500  hover:bg-violet-500/10  dark:text-violet-200  dark:hover:bg-violet-500/20',
          purple:  'text-purple-500  hover:bg-purple-500/10  dark:text-purple-200  dark:hover:bg-purple-500/20',
          fuchsia: 'text-fuchsia-500 hover:bg-fuchsia-500/10 dark:text-fuchsia-200 dark:hover:bg-fuchsia-500/20',
          pink:    'text-pink-500    hover:bg-pink-500/10    dark:text-pink-200    dark:hover:bg-pink-500/20',
          rose:    'text-rose-500    hover:bg-rose-500/10    dark:text-rose-200    dark:hover:bg-rose-500/20',
        }
      },
      defaultVariants: {
        size: 'md',
        color: 'zinc',
      },
    }
)
export type GhostButtonVariants = VariantProps<typeof ghostButtonVariants>

export const linkButtonVariants = cva(
    cn(baseClass, 'text-current underline-offset-4 hover:underline'),
    {
      variants: {
        size: sizes,
        color: {
          zinc:    'hover:text-zinc -500   dark:hover:text-zinc-400    focus-visible:ring-zinc-500/50',
          red:     'hover:text-red-500     dark:hover:text-red-400     focus-visible:ring-red-500/50',
          orange:  'hover:text-orange-500  dark:hover:text-orange-400  focus-visible:ring-orange-500/50',
          amber:   'hover:text-amber-500   dark:hover:text-amber-400   focus-visible:ring-amber-500/50',
          yellow:  'hover:text-yellow-500  dark:hover:text-yellow-400  focus-visible:ring-yellow-500/50',
          lime:    'hover:text-lime-500    dark:hover:text-lime-400    focus-visible:ring-lime-500/50',
          green:   'hover:text-green-500   dark:hover:text-green-400   focus-visible:ring-green-500/50',
          emerald: 'hover:text-emerald-500 dark:hover:text-emerald-400 focus-visible:ring-emerald-500/50',
          teal:    'hover:text-teal-500    dark:hover:text-teal-400    focus-visible:ring-teal-500/50',
          cyan:    'hover:text-cyan-500    dark:hover:text-cyan-400    focus-visible:ring-cyan-500/50',
          sky:     'hover:text-sky-500     dark:hover:text-sky-400     focus-visible:ring-sky-500/50',
          blue:    'hover:text-blue-500    dark:hover:text-blue-400    focus-visible:ring-blue-500/50',
          indigo:  'hover:text-indigo-500  dark:hover:text-indigo-400  focus-visible:ring-indigo-500/50',
          violet:  'hover:text-violet-500  dark:hover:text-violet-400  focus-visible:ring-violet-500/50',
          purple:  'hover:text-purple-500  dark:hover:text-purple-400  focus-visible:ring-purple-500/50',
          fuchsia: 'hover:text-fuchsia-500 dark:hover:text-fuchsia-400 focus-visible:ring-fuchsia-500/50',
          pink:    'hover:text-pink-500    dark:hover:text-pink-400    focus-visible:ring-pink-500/50',
          rose:    'hover:text-rose-500    dark:hover:text-rose-400    focus-visible:ring-rose-500/50',
        }
      },
      defaultVariants: {
        size: 'md',
        color: 'zinc',
      },
    }
)
export type LinkButtonVariants = VariantProps<typeof linkButtonVariants>
