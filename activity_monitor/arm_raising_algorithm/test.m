%create an array where each row represent one cell of the blocks array
% the first column is the min time, the second column is the max time of
% the individual cell.


minmax = [ 0 0 ];
forward = [0];
[rows, cols] = size(blocks);
for i = 1:cols
    minmax(i,1) = blocks{i}(1,1);
    [rows, cols] = size(blocks{i});
    minmax(i,2) = blocks{i}(rows,1);
    forward(i) = blocks{i}(1, 3) > 0;
end

[rows, cols] = size(minmax);
diff = [0];
for i = 1:rows-1
    diff(i) = minmax(i+1,1) - minmax(i,1);
end

len = [0];
for i = 1:rows
    len(i) = minmax(i,2) - minmax(i,1);
end


    