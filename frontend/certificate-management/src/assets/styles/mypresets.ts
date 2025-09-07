import { definePreset } from '@primeuix/themes';
import Aura from '@primeuix/themes/aura';

const myPalette = {
    blue: {
        '50': '#F0F2FB',
        '100': '#D8DDF5',
        '200': '#B9C0EB',
        '300': '#949CDD',
        '400': '#6A74CF',
        '500': '#3B5AC2',
        '600': '#314BAA',
        '700': '#273C8D',
        '800': '#1F2F70',
        '900': '#1A2856', 
        '950': '#121A3A'
    },
    gray: {
        '50': '#F8FAFC',
        '100': '#F1F5F9',
        '200': '#E2E8F0', 
        '300': '#CBD5E1',
        '400': '#94A3B8',
        '500': '#64748B',
        '600': '#475569',
        '700': '#334155',
        '800': '#1E293B',
        '900': '#121621', 
        '950': '#0D1017'
    },
    danger: {
        '500': '#E95F5F', 
        '600': '#D71D1D' 
    }
};

const customPreset = definePreset(Aura, {
    palette: {
        ...myPalette
    },
    semantic: {
        primary: {
            50: '{blue.50}',
            100: '{blue.100}',
            200: '{blue.200}',
            300: '{blue.300}',
            400: '{blue.400}',
            500: '{blue.500}',
            600: '{blue.600}',
            700: '{blue.700}',
            800: '{blue.800}',
            900: '{blue.900}',
            950: '{blue.950}',
        },
        colorScheme: {
            light: {
                primary: {
                    color: '#3B5AC2',
                    inverseColor: '#ffffff',
                    hoverColor: '{blue.600}',
                    activeColor: '{blue.700}',
                },
                highlight: {
                    background: '{blue.500}',
                    focusBackground: '{blue.700}',
                    color: '#ffffff',
                    focusColor: '#ffffff',
                },
                surface: {
                    0: '#ffffff',
                    50: '{gray.50}',
                    100: '{gray.100}',
                    200: '{gray.200}',
                    300: '{gray.300}',
                    400: '{gray.400}',
                    500: '{gray.500}',
                    600: '{gray.600}',
                    700: '{gray.700}',
                    800: '{gray.800}',
                    900: '{gray.900}',
                    950: '{gray.950}'
                }
            },
            // Você pode customizar o tema escuro (dark mode) aqui também
            dark: {
                primary: {
                    color: '{blue.400}',
                    inverseColor: '{blue.950}',
                    hoverColor: '{blue.300}',
                    activeColor: '{blue.200}',
                },
                highlight: {
                    background: '{blue.500}',
                    focusBackground: '{blue.700}',
                    color: '#ffffff',
                    focusColor: '#ffffff'
                }
            },
        },
    },
});

export default customPreset;