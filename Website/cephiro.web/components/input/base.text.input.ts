import styled from 'styled-components'

const Input = {
    TextArea: 'textArea',
    Text: 'text',

}

export const InputBox = styled.div`
    display: flex;
    flex-direction: row;
    gap: 0.7rem;
    position: relative;
    svg:first-child {
        position: absolute;
        top:50%; 
        left:1rem; 
        transform:translate(-50%, -50%);
        color: ${props => props.theme.default12};
    }
`

interface InputProps {
    withIcon: Boolean;
}

export const TextInput = styled.input<InputProps>`
    background-color: transparent;
    border-radius: 0.375rem;
    width: 100%;
    display: block;
    transition: box-shadow .3s ease-in-out;
    padding: 0.5rem ${props => props.withIcon ? '2rem' : '1.25rem'};
    font-size: .875rem;
    line-height: 1.25rem;
    border: 1px solid ${props => props.theme.default7};
    outline: none;
    writing-mode: horizontal-tb !important;
    &::before, &::after {
        box-sizing: border-box;
    }
    font-size: 0.8rem;
    color: ${props => props.theme.default12};
    &:hover {
        outline-color: ${props => props.theme.primary3};
        border: 1px solid ${props => props.theme.primary1};
    }


    &:focus
    {
        border: 1px solid ${props => props.theme.primary3};
        outline: 1px ${props => props.theme.primary1};
        box-shadow: 0 0 1px ${props => props.theme.primary5}, 0 0 3px ${props => props.theme.primary5}, 0 0 6px ${props => props.theme.primary5};
    }

    &:invalid {
        color: ${props => props.theme.danger3};
        background-color: ${props => props.theme.danger10};
        border: 1px solid ${props => props.theme.danger3};
        outline: 1px ${props => props.theme.danger1};
        box-shadow: 0 0 1px ${props => props.theme.danger5}, 0 0 3px ${props => props.theme.danger5}, 0 0 6px ${props => props.theme.danger5};
    }

    &:valid {
        color: ${props => props.theme.primary1};
        background-color: ${props => props.theme.primary10};
        border: 1px solid ${props => props.theme.primary3};
        outline: 1px ${props => props.theme.primary1};
        box-shadow: 0 0 1px ${props => props.theme.primary5}, 0 0 3px ${props => props.theme.primary5}, 0 0 6px ${props => props.theme.primary5};
    }

    &::-webkit-outer-spin-button,
    &::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0;
    }

    &[type=number] {
        -moz-appearance: textfield;
    }
`