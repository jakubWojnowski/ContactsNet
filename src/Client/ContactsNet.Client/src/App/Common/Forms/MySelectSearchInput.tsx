import React from 'react';
import { useField } from 'formik';
import {Dropdown, Form, Label} from 'semantic-ui-react';

interface Props {
    placeholder: string;
    name: string;
    options: { text: string; value: string }[];
    label?: string;
}

const MySelectSearchInput: React.FC<Props> = (props) => {
    const [field, meta, helpers] = useField(props.name);

    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <label>{props.label}</label>
            <Dropdown
                fluid
                multiple
                search
                selection
                options={props.options}
                value={field.value || ''}
                onChange={(_, data) => helpers.setValue(data.value)}
                onBlur={() => helpers.setTouched(true)}
                placeholder={props.placeholder}
            />
            {meta.touched && meta.error && (
                <Label basic color='red' pointing>
                    {meta.error}
                </Label>
            )}
        </Form.Field>
    );
};

export default MySelectSearchInput;
