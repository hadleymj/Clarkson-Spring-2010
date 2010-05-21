function count = count_steps(diff)

[rows, cols] = size(diff);
len = max(rows,cols);
zero_count = 0;
count = 0;
for i = 1:len
    if ( zero_count >= 5 && diff(i) > 0 )
        count = count + 1;
        zero_count = 0;
    end
    if ( diff(i) == 0 )
        zero_count = zero_count + 1;
    end
end


    