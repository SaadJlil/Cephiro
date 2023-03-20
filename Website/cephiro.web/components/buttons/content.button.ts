import styled, { css } from 'styled-components';

interface ButtonContentProps
{
    gap?: string;
    alignItems?: string;
    direction?: string;
    justifyContent?: string;
}

export const ButtonContent = styled.div<ButtonContentProps>`
    display: flex;
    flex-direction: ${props => props.direction};
    gap: ${props => props.gap};
    align-items: ${props => props.alignItems};
    justify-content: ${props => props.justifyContent};
`

