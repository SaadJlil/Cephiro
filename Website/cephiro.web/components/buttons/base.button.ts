import styled from 'styled-components'
import {SetSize, Size} from '../common/size'
import { ButtonVariant, SetButton } from './options.button';

interface ButtonProp {
    size?: Size | undefined;
    button?: ButtonVariant | undefined;
    block?: Boolean | undefined;
}

// add icons left & right
export const Button = styled.button<ButtonProp>`
    border-radius: 0.25rem;
    outline: 2px solid transparent;
    outline-offset: 2px;
    border: 1px solid transparent;
    width: ${props => props.block ? '100%' : 'null'};
    display: block;
    font-weight: 500;
    ${props => SetSize(props.size)};
    ${props => SetButton(props.button)};
    &:hover {
        cursor: pointer;
    }
`

