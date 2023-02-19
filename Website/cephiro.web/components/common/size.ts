import { css } from 'styled-components'

export declare type Size = "tiny" | "small" | "medium" | "large" | "xlarge"

const Tiny = css`
    padding: 0.375rem 0.625rem;
    font-size: 0.75rem;
    line-height: 1rem;
`

const Small = css`
    padding: 0.5rem 0.750rem;
    font-size: 0.875rem;
    line-height:1rem;
`

const Medium = css`
    padding: 0.5rem 1rem;
    font-size: 0.875rem;
    line-height: 1.25rem;
`

const Large = css`
    padding:  0.5rem 1rem;
    font-size: 1rem;
    line-height: 1.5rem;
`

const XLarge = css`
    padding: 0.75rem 1.5rem;
    font-size: 1rem;
    line-height: 1.5rem;
`

export const SetSize = (size?: Size | undefined) => {
    switch (size)
    {
        case "tiny": return Tiny
        case "small": return Small
        case "medium": return Medium
        case "large": return Large
        case "xlarge": return XLarge

        default: return Medium
    }
}