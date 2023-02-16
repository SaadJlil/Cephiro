import { css } from "styled-components"

export declare type ButtonVariant = "primary" | "secondary" | "default" | "text"

const PrimaryButton = css`
    background-color: ${props => props.theme.primary4};
    color: ${props => props.theme.default13};
    &:hover {
        background-color: ${props => props.theme.primary3};
        box-shadow: 0 0 1px ${props => props.theme.primary5}, 0 0 4px ${props => props.theme.primary5}, 0 0 8px ${props => props.theme.primary5};
    }
    &:active {
        background-color: ${props => props.theme.primary5};
        box-shadow: none;
    }
`

const SecondaryButton = css`
    background-color: ${props => props.theme.secondary1};
    color: ${props => props.theme.primary6};
    &:hover {
        background-color: ${props => props.theme.primary3};
        color: ${props => props.theme.default13};
    }
    &:active {
        background-color: ${props => props.theme.primary2};
        color: ${props => props.theme.default13};
    }
`

const DefaultButton = css`
    background-color: ${props => props.theme.default4};
    border-color: ${props => props.theme.default4};
    color: ${props => props.theme.default12};
    &:hover {
        background-color: ${props => props.theme.default12};
        border-color: ${props => props.theme.default12};
        color: ${props => props.theme.default4};
    }
`

const TextButton = css`
    border-color: transparent;
    background: transparent;
    color: ${props => props.theme.default12};
    &:hover {
        color: ${props => props.theme.default13};
        background-color: ${props => props.theme.default3};
    }
    &:active {
        border-color: ${props => props.theme.default6};
    }
`

export const SetButton = (button?: ButtonVariant | undefined) => {
    switch (button)
    {
        case "primary": return PrimaryButton
        case "secondary": return SecondaryButton
        case "default": return DefaultButton
        case "text": return TextButton

        default: return DefaultButton
    }
}